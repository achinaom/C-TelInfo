using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using DAL.models;
namespace DAL
{
    public class ContributionDAL : IContributionDAL
    {
        //שלית כל התרומות ממסד הנתונים
        public List<Contribution> GetContribution()
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                var ContributionList = context.Contribution.ToList();
                return ContributionList;
            }

        }
        //הוספת תרומה חדשה
        public void AddContribution(Contribution contribution)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                context.Contribution.Add(contribution);
                context.SaveChanges();
            }
        }
        //מחיקת תרומה
        public void DeleteContribution(int id)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                Contribution contribution = context.Contribution.Where(c => c.Id == id).FirstOrDefault();
                if (contribution != null)
                {
                    context.Contribution.Remove(contribution);
                    context.SaveChanges();
                }
            }
        }
        //עדכון תרומה
        public void updateContribution(Contribution contribution)
        {
            using (var context = new CUSERS1DESKTOPDATABASEMARKETINMDFContext())
            {
                context.Contribution.Update(contribution);
                context.SaveChanges();
            }
        }
    }
}
