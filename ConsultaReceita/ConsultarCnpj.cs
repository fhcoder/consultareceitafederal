using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Web;

namespace ConsultaReceita
{
    /// <summary>
    /// Auxilia no processo de consulta do cnpj na Receita Federal
    /// </summary>
    public sealed class ConsultarCnpj
    {

        public readonly CookieContainer cookieContainer = new CookieContainer();
        private string urlBase = "http://www.receita.fazenda.gov.br/pessoajuridica/cnpj/cnpjreva/Cnpjreva_Solicitacao2.asp";
        private string urlCaptcha = "http://www.receita.fazenda.gov.br/pessoajuridica/cnpj/cnpjreva/captcha/gerarCaptcha.asp";
        private string urlPostConsulta = "http://www.receita.fazenda.gov.br/pessoajuridica/cnpj/cnpjreva/valida.asp";
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
            /// <param name="numeroCnpj">Numero do cnpj, apenas digitos</param>
            /// <param name="captcha">Codigo do captcha</param>
            /// <returns>Retorna um objeto do tipo Cnpj com todos os dados fornecidos pela consulta.</returns>
        public Cnpj Consultar(string numeroCnpj, string captcha)
        {
            Cnpj cnpj = new Cnpj();

            string parametros = "origem=comprovante&cnpj="+ HttpUtility.UrlEncode(numeroCnpj)+"&txtTexto_captcha_serpro_gov_br="+HttpUtility.UrlEncode(captcha)+"&submit1=Consultar&search_type=cnpj";
            byte[] byteArray = Encoding.UTF8.GetBytes(parametros);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(urlPostConsulta);
            request.CookieContainer = cookieContainer;
            request.ContentType = "application/x-www-form-urlencoded";
            request.Method = "POST";
            request.UserAgent = "Mozilla/5.0 (compatible; Synapse)";
            request.ContentLength = parametros.Length;
            Stream sw = request.GetRequestStream();
            sw.Write(byteArray, 0, byteArray.Length);
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string htmlText = "";
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default))
            {
               htmlText = sr.ReadToEnd();
            }
            
            cnpj.HtmlDaPagina = htmlText;
            string[] arrConsulta = GetArrayData(htmlText);
            if (arrConsulta.Length >= 22)
            {
                cnpj.NumeroDeInscricao = arrConsulta[0];
                cnpj.DataDeAbertura = arrConsulta[1];
                cnpj.NomeEmpresarial = arrConsulta[2];
                cnpj.NomeFantasia = arrConsulta[3];
                cnpj.AtividadeEconomicaPrincipal = arrConsulta[4];
                cnpj.AtividadeEconomicaSecundaria = arrConsulta[5];
                cnpj.CodigoDescricaoDaNaturezaJuridica = arrConsulta[6];
                cnpj.Logradouro = arrConsulta[7];
                cnpj.NumeroEndereco = arrConsulta[8];
                cnpj.Complemento = arrConsulta[9];
                cnpj.Cep = arrConsulta[10];
                cnpj.BairroDistrito = arrConsulta[11];
                cnpj.Municipio = arrConsulta[12];
                cnpj.UF = arrConsulta[13];
                cnpj.Email = arrConsulta[14];
                cnpj.Telefone = arrConsulta[15];
                cnpj.EFR = arrConsulta[16];
                cnpj.SituacaoCadastral = arrConsulta[17];
                cnpj.DataDaSituacaoCadastral = arrConsulta[18];
                cnpj.MotivoSituacaoCadastral = arrConsulta[19];
                cnpj.SituacaoEspecial = arrConsulta[20];
                cnpj.DataDaSituacaoEspecial = arrConsulta[21];
            }
            return cnpj;
        }
        private string[] GetArrayData(string html)
        {
            HtmlAgilityPack.HtmlDocument doc = new HtmlAgilityPack.HtmlDocument();
            string[] result;
            doc.LoadHtml(html);
            HtmlNodeCollection itens = doc.DocumentNode.SelectNodes("//font[@style='font-size: 8pt']");
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
                string elem = Regex.Replace(itens[i].InnerText, @"\t|\n|\r", ""); 
                result[i] = elem;
            }
            return result;
        }
    }
}
