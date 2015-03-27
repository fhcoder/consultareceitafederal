using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Drawing;
using System.Web;

namespace ConsultaReceita
{
    /// <summary>
    /// Auxilia no processo de consulta a situação cadastral do cpf na Receita Federal
    /// </summary>
    public sealed class ConsultarCpf
    {
        
        public readonly CookieContainer cookieContainer = new CookieContainer();
        private string urlBase = "http://www.receita.fazenda.gov.br/aplicacoes/atcta/cpf/ConsultaPublica.asp";
        private string urlCaptcha = "http://www.receita.fazenda.gov.br/aplicacoes/atcta/cpf/captcha/gerarCaptcha.asp";
        private string urlPostConsulta = "http://www.receita.fazenda.gov.br/aplicacoes/atcta/cpf/ConsultaPublicaExibir.asp";

        /// <summary>
        /// Obtem o capcha necessário para validação da consulta.
        /// </summary>
        /// <returns>Retorna um objeto System.Drawing.Image</returns>
        public Image GetCaptcha()
        {
            string htmlResult = string.Empty;
            using (var wc = new CookieAwareWebClient())
            {
                wc.CookieContainer = this.cookieContainer;
                wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; Synapse)";
                wc.Headers[HttpRequestHeader.KeepAlive] = "300";
                htmlResult = wc.DownloadString(this.urlBase);
            }
            if (htmlResult != string.Empty)
            {
                using (var wc = new CookieAwareWebClient())
                {
                    wc.CookieContainer = this.cookieContainer;
                    wc.Headers[HttpRequestHeader.UserAgent] = "Mozilla/4.0 (compatible; Synapse)";
                    wc.Headers[HttpRequestHeader.KeepAlive] = "300";
                    byte[] data = wc.DownloadData(this.urlCaptcha);
                    return Image.FromStream(new MemoryStream(data));
                }
            }
            return null;

        }
        /// <summary>
        /// Executa a consulta
        /// </summary>
        /// <param name="numeroCpf">Cpf, apenas digitos</param>
        /// <param name="captcha">Texto do captcha</param>
        /// <returns>Retorna um objeto Consulta, contendo os dados de uma consulta.</returns>
        public Cpf Consultar(string numeroCpf, string captcha)
        {
            Cpf cpf = new Cpf();

            string parametros = "txtCPF=" + HttpUtility.UrlEncode(numeroCpf) + "&txtTexto_captcha_serpro_gov_br=" + HttpUtility.UrlEncode(captcha) + "&Enviar=Consultar";
            byte[] byteArray = Encoding.UTF8.GetBytes(parametros);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(this.urlPostConsulta);
            request.CookieContainer = cookieContainer;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.UserAgent = "Mozilla/4.0 (compatible; Synapse)";
            request.ContentLength = parametros.Length;
            Stream sw = request.GetRequestStream();
            sw.Write(byteArray, 0, byteArray.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string htmlText = string.Empty;
           using(StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default))
           {
             htmlText = sr.ReadToEnd();
           }
             
            cpf.HtmlDaPagina = htmlText;
            string[] arrConsulta = GetArrayData(htmlText);
            if (arrConsulta.Length >= 4)
            {
                cpf.Numero = arrConsulta[0];
                cpf.Nome = arrConsulta[1];
                cpf.SituacaoCadastral = arrConsulta[2];
                cpf.DigitoVerificador = arrConsulta[3];
            }
            return cpf;
        }

        private string[] GetArrayData(string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            string[] result;
            doc.LoadHtml(html);
            HtmlNodeCollection itens = doc.DocumentNode.SelectNodes("//span[@class='clConteudoDados']");
            if (itens != null)
            {
                result = new string[itens.Count];
            }
            else
            {
                result = new string[0];
            }
            for (int i = 0; i < result.Length; i++)
            {
                string elem = itens[i].InnerText;
                int startIndex = elem.IndexOf(":");
                result[i] = elem.Substring(startIndex + 1);
            }
            return result;
        }
    }
}
