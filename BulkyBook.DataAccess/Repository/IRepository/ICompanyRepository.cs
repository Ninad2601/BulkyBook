using BulkyBook.Models;


namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface ICompanyRepository : IRepository<Company>
    {
        //Remember to implement some model/class specific methods over its interface

        void Update(Company objCategory);
       
    }
}
