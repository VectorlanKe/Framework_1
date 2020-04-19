using IRepository;
using IServices;
using Microsoft.Extensions.Configuration;
using RepositoryFactory;
using RepositoryModel;
using System;
using System.Collections.Generic;
using System.Text;

namespace Services
{
    public class AccountService: BaseService<Account>, IAccountService
    {
        public AccountService(IConfiguration configuration) 
            :base(configuration) { }
    }
}
