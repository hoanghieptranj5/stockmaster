using System;
using System.Collections.Generic;
using Cp68Minion.Models;
using StockMaster.Contracts;
using StockMaster.Models.CoPhieu69;
using StockMaster.Services.Files;

namespace Cp68Minion
{
    public class Cp68StockInfoMinion : MinionBase
    {
        private readonly FileService _fileService;

        public Cp68StockInfoMinion(FileService fileService)
        {
            _fileService = fileService;
        }

        protected override void MainMethod()
        {
            var companyList = new CompanyListModel
            {
                Stc = Stc.StcIds[0],
                Companies = new List<Company>()
            };
            
            throw new NotImplementedException();
        }
    }
}