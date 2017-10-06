namespace ByTheCakeApplication.Context.Contracts
{
    using System.Collections.Generic;
    using Models;

    public interface IContext
    {
        void InitializeDb(bool deleteDatabaseIfExistent);

        string Add(Cake cake);

        ICollection<Cake> GetAll();
    }
}
