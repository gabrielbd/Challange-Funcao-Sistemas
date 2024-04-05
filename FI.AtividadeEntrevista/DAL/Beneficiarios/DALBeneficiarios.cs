using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace FI.AtividadeEntrevista.DAL.Beneficiarios
{
    internal class DALBeneficiarios : AcessoDados
    {

        internal long Created(Beneficiario beneficiario)
            {
                List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

                parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", beneficiario.Nome));
                parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", beneficiario.CPF));
                parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", beneficiario.IdCliente));


                DataSet ds = base.Consultar("CreateBeneficiario", parametros);
                long ret = 0;
                if (ds.Tables[0].Rows.Count > 0)
                    long.TryParse(ds.Tables[0].Rows[0][0].ToString(), out ret);
                return ret;
            }
        internal Beneficiario GetById(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            DataSet ds = base.Consultar("GetByIdBeneficiario", parametros);
            List<Beneficiario> benef = Converter(ds);

            return benef.FirstOrDefault();
        }
        internal List<Beneficiario> GetByIdCliente(long idCliente)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("IDCLIENTE", idCliente));

            DataSet ds = base.Consultar("GetByIdClienteBeneficiario", parametros);
            List<Beneficiario> cli = Converter(ds);

            return cli;
        }

        internal void Update(Beneficiario benef)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Nome", benef.Nome));
            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", benef.CPF));
            parametros.Add(new System.Data.SqlClient.SqlParameter("ID", benef.Id));

            base.Executar("UpdateBeneficiario", parametros);
        }

        internal void Excluir(long Id)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("Id", Id));

            base.Executar("DeleteBeneficiario", parametros);
        }


        internal bool VerificarExistencia(string CPF)
        {
            List<System.Data.SqlClient.SqlParameter> parametros = new List<System.Data.SqlClient.SqlParameter>();

            parametros.Add(new System.Data.SqlClient.SqlParameter("CPF", CPF));

            DataSet ds = base.Consultar("ValidationCpfBeneficiario", parametros);

            return ds.Tables[0].Rows.Count > 0;
        }

        private List<Beneficiario> Converter(DataSet ds)
        {
            List<Beneficiario> lista = new List<Beneficiario>();

            if (ds != null && ds.Tables != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    Beneficiario benef = new Beneficiario();
                        benef.Id = row.Field<long>("Id");
                        benef.Nome = row.Field<string>("Nome");
                        benef.CPF = row.Field<string>("CPF");
                        benef.IdCliente = row.Field<long>("IDCLIENTE");
                    lista.Add(benef);
                }
            }

            return lista;
        }
    }
}
