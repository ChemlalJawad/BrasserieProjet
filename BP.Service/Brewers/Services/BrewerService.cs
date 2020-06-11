﻿using BP.Core.Domains;
using BP.Data.Repositories.Interfaces;
using BP.Service.Brewers.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BP.Service.Brewers.Services
{
    public class BrewerService : IBrewerService
    {
        private readonly IBrewerRepository _brewerRepository;

        public BrewerService(IBrewerRepository brewerRepository)
        {
            _brewerRepository = brewerRepository;
        }

        public Brewer FindBrewerById(int Id)
        {
            var brewer = _brewerRepository.FindBrewerById(Id);
            return brewer;
        }

        public IEnumerable<Brewer> GetAllBeers()
        {
            var brewers = _brewerRepository.GetAllBeers();
            return brewers;
        }
    }
}
