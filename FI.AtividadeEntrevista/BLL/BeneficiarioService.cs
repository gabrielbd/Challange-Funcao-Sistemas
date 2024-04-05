using FI.AtividadeEntrevista.DAL.Beneficiarios;
using FI.AtividadeEntrevista.DML;
using System.Collections.Generic;

namespace FI.AtividadeEntrevista.BLL
{
    public class BeneficiarioService
    {
        public long Created(Beneficiario beneficiario)
        {
            DALBeneficiarios benef = new DALBeneficiarios();
            return benef.Created(beneficiario);
        }

        public void Update(Beneficiario beneficiario)
        {
            DALBeneficiarios benef = new DALBeneficiarios();
            benef.Update(beneficiario);
        }

        public Beneficiario GeById(long id)
        {
            DALBeneficiarios benef = new DALBeneficiarios();
            return benef.GetById(id);
        }
        public List<Beneficiario> GetByIdCliente(long idCliente)
        {
            DALBeneficiarios benef = new DALBeneficiarios();
            return benef.GetByIdCliente(idCliente);
        }

        public bool VerificarExistencia(string CPF)
        {
            DALBeneficiarios benef = new DALBeneficiarios();
            return benef.VerificarExistencia(CPF);
        }
        public void Excluir(long id)
        {
            DALBeneficiarios benef = new DALBeneficiarios();
            benef.Excluir(id);
        }


    }
}
