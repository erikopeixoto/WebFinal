using System;

namespace WEBTeste.Util
{
    public class TratarString
    {
        public string RetirarAcentos(string pString)
        {
            string LdsRetorno;

            LdsRetorno = pString.Replace("Á", "A").Replace("Ã", "A").Replace("Â", "A").Replace("Ê", "E").Replace("É", "E").Replace("Í", "I").Replace("Õ", "O");
            LdsRetorno = LdsRetorno.Replace("Ó", "O").Replace("Ô", "O").Replace("Ú", "U").Replace("Ç", "C");
            return LdsRetorno;

        }

        public string RetirarEspecial(string pString)
        {
            string LdsRetorno = "";
            int i, LcodAsc;

            for (i = 0;i < pString.Length;i++)
            {
                LcodAsc = (int) pString[i] + 0;
                if ((LcodAsc > 64 && LcodAsc < 91) || LcodAsc == 32)
                    LdsRetorno += pString.Substring( i, 1);
            }
            return LdsRetorno;
        }

        public string RetirarEspLetra(string pNumero)
        {
            string LdsRetorno = "";
            int i, LcodAsc;

            for (i = 0; i < pNumero.Length; i++)
            {
                LcodAsc = (int)pNumero[i] + 0;
                if ((LcodAsc >= 48 && LcodAsc < 58))
                    LdsRetorno += pNumero.Substring(i, 1);
            }
            return LdsRetorno;
        }

        public string RetirarEspacos(string pString)
        {
            string LdsRetorno = "";
            string[] LdsAssunto;
            int i, LqtPalavra;
            try
            {
                LqtPalavra = 0;
                LdsAssunto = pString.Split(' ');
                for (i = 0; i < LdsAssunto.Length - 1; i++)
                {
                    if (LdsAssunto.GetValue(i).ToString().Length > 4)
                    {
                        LdsRetorno = LdsRetorno + LdsAssunto.GetValue(i).ToString();
                        LqtPalavra = LqtPalavra + 1;
                    }
                    if (LqtPalavra == 2)
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return LdsRetorno;

        }

        public string[] SepararPalavra(string pString)
        {
            string[] LdsRetorno;
            string[] LstPalavra;
            int i, LqtPalavra;
            try
            {
                
                LqtPalavra = 0;
                LstPalavra = pString.Split(' ');
                LdsRetorno = new string[LstPalavra.Length];

                for (i = 0; i < LstPalavra.Length; i++)
                {
                    LdsRetorno.SetValue("", i);
                    if (LstPalavra.GetValue(i).ToString().Length >= 3)
                    {
                        LdsRetorno.SetValue(LstPalavra.GetValue(i).ToString(), LqtPalavra);
                        LqtPalavra = LqtPalavra + 1;
                    }
                    if (LqtPalavra == 3)
                        break;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return LdsRetorno;

        }
    }  
}