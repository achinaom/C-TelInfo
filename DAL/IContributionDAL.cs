using System;
using System.Collections.Generic;
using System.Text;
using DAL.models;
namespace DAL
{
   public interface IContributionDAL
    {


        public void AddContribution(Contribution contribution);
        public void DeleteContribution(int id);
        public void updateContribution(Contribution contribution);
        public List<Contribution> GetContribution();

    }
}
