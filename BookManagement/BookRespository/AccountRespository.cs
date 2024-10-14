using BookBussinessObject;
using BookDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookRespository
{
    public class AccountRespository : IAccountRespository
    {
        public Account GetAccount(string username, string password) => AccountDAO.Instance.GetAccount(username, password);

        public List<Account> GetAllAccounts() => AccountDAO.Instance.GetAllAccounts();
        public bool deleteAccount(int id) => AccountDAO.Instance.deleteAccount(id);
        public void updateAccount(Account account) => AccountDAO.Instance.updateAccount(account);
    }
}
