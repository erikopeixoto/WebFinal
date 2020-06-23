using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;
using Oracle.DataAccess.Client;

namespace AcessoDados
{
    public class cnxOracle
    {
        static string DataSource = "ORACLE";
        static string Provider = "OraOLEDB.Oracle"; //"msdaora";
        static string Login = "apeixoto";
        static string Senha = "ae600946";
        private string ConexaoOledb = "Provider=" + Provider +
                                      ";Data Source=" + DataSource + 
                                      ";User Id=" + Login + 
                                      ";Password=" + Senha;

        public Oracle.DataAccess.Client.OracleConnection RetornaCnx()
        {
            string ConexaoCliente = "Data Source=" + DataSource + ";User Id=" + Login + ";Password=" + Senha;
            Oracle.DataAccess.Client.OracleConnection cnx = new Oracle.DataAccess.Client.OracleConnection(ConexaoCliente);
            return cnx;
        }

        public OleDbConnection RetornaCnxOledb()
        {
            OleDbConnection cnx = new OleDbConnection(ConexaoOledb);
            return cnx;
        }

        public DataSet AtualizaBanco(DataSet Pds, string Psql)
        {

            System.Data.OleDb.OleDbConnection cnx = new System.Data.OleDb.OleDbConnection(ConexaoOledb);
            try
            {
                System.Data.OleDb.OleDbDataAdapter Adp = new System.Data.OleDb.OleDbDataAdapter();
                Adp.SelectCommand = new System.Data.OleDb.OleDbCommand(Psql, cnx);
                System.Data.OleDb.OleDbCommandBuilder cb = new System.Data.OleDb.OleDbCommandBuilder(Adp);

                cnx.Open();

                Adp.Update(Pds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnx.Close();
            }

            return Pds;
        }

        public DataSet RetornaDataSet(string psql)
        {
            System.Data.OleDb.OleDbConnection cnx = new System.Data.OleDb.OleDbConnection(ConexaoOledb);
            System.Data.OleDb.OleDbDataAdapter Adp = new System.Data.OleDb.OleDbDataAdapter(psql, cnx);
            DataSet ds = new DataSet();

            try
            {

                Adp.Fill(ds);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnx.Close();
            }

            return ds;
        }

        public bool ExecutaComando(string psql)
        {
            System.Data.OleDb.OleDbConnection cnx = new System.Data.OleDb.OleDbConnection(ConexaoOledb);
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(psql, cnx);
            bool wret = true;

            try
            {
                cnx.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox(ex.Message.ToString(), "TS_ESTATISTICAS");
            }
            finally
            {
                cnx.Close();
            }

            return wret;
        }

        public string ExecutaSP(string psql, string[] param1)
        {
            System.Data.OleDb.OleDbConnection cnx = new System.Data.OleDb.OleDbConnection(ConexaoOledb);
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(psql, cnx);
            string wret = "";

            try
            {
                cnx.Open();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = psql;
                for (int i = 0; i < param1.Length - 1; i++)
                {
                    cmd.Parameters.Add("@" + param1.GetValue(i).ToString(), OracleDbType.Varchar2).Value = param1.GetValue(i).ToString();
                }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return wret;
        }

        public OleDbParameter CreateOleDbParameter(string nome, object valor, DbType? tipo, int? tamanho, ParameterDirection? direcao)
        {
            OleDbParameter parameter = new OleDbParameter(nome, valor);
            if (tipo != null)
            {
                parameter.DbType = (DbType)tipo;
            }
            if (tamanho != null)
            {
                parameter.Size = (int)tamanho;
            }
            if (direcao != null)
            {
                parameter.Direction = (ParameterDirection)direcao;
            }

            return parameter;
        }

        public string ExecutaSP(string psql, OleDbParameter[] parametros)
        {
            System.Data.OleDb.OleDbConnection cnx = new System.Data.OleDb.OleDbConnection(ConexaoOledb);
            System.Data.OleDb.OleDbCommand cmd = new System.Data.OleDb.OleDbCommand(psql, cnx);
            string wret = "";

            try
            {
                cnx.Open();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = psql;
                cmd.Parameters.AddRange(parametros);
                
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return wret;
        }

        public string ExecutaSpLogin(string pUsuario, string ptxtSenha)
        {
            OracleConnection cnx = new OracleConnection(ConexaoOledb);
            OracleCommand cmd = new OracleCommand();

            try
            {
                cnx.Open();
                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "TS_VALIDA_LOGIN";
                cmd.Parameters.Add("@pcodUsuario", OracleDbType.Varchar2, 15).Value = pUsuario;
                cmd.Parameters.Add("@pSenha", OracleDbType.Varchar2, 50).Value = ptxtSenha;
                cmd.Parameters.Add("@pOperadora", OracleDbType.Varchar2, 15).Value = '1';
                //cmd.Parameters.Add("@pIp", Oracle.DataAccess.Client.OracleDbType.Varchar2, 15).Value = ' ';
                cmd.Parameters.Add("@pnumRetorno", OracleDbType.Varchar2, 3).Direction = ParameterDirection.Output;
                cmd.Parameters.Add("@pmsgRetorno",OracleDbType.Varchar2, 255).Direction = ParameterDirection.Output;
                cmd.ExecuteNonQuery();

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                cnx.Close();
                if (cmd.Parameters[4].Value.ToString() != "null")
                {
                    //MessageBox(cmd.Parameters[4].Value.ToString(), "TS_ESTATISTICAS");
                }
            }
            return cmd.Parameters[3].Value.ToString();
        }

        public DataView RetornaDataVw(string psql)
        {
            System.Data.OleDb.OleDbConnection cnx = new System.Data.OleDb.OleDbConnection(ConexaoOledb);
            System.Data.OleDb.OleDbDataAdapter Adp = new System.Data.OleDb.OleDbDataAdapter(psql, cnx);
            DataSet ds = new DataSet();
            DataView dv = new DataView();

            try
            {

                Adp.Fill(ds);
                dv = ds.Tables[0].DefaultView;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            cnx.Close();
            return dv;
        }
    }
}
