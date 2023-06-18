using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataLayer.UnitOfWork;
using System.Linq.Expressions;
using DataLayer.Models;
using AutoMapper;
using BusinessLayer.Exceptions;
using BusinessLayer.DTOs;
using DataLayer;

namespace BusinessLayer.Services.AccountService
{
    public class AccountService:IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }
        public async Task<ObjectResult> GetAccountById(int id)
        {
            var expressions = new List<Expression<Func<Account, bool>>>()
            {
                x => x.AccountId == id,
            };
            var account =(await _unitOfWork.AccountRepository.GetAccountsWithAddresses(expressions, Constant.PAGE_SINGLE_ITEM)).FirstOrDefault();
            if (account == null)
            {
                throw new IdNotFoundException($"Account Id: {id} not found!");
            }
            var accountResponse = _mapper.Map<AccountResponse>(account);
            return new ObjectResult(accountResponse);
        }
        public async Task<ObjectResult> GetAccountByFullName(string fullName, Paging page)
        {
            var expressions = new List<Expression<Func<Account, bool>>>()
            {
                x => x.FullName.ToLower().Contains(fullName.ToLower()),
            };
            var accounts = await _unitOfWork.AccountRepository.GetAccountsWithAddresses(expressions, page);
            var accountResponses = accounts.Select(x => _mapper.Map<AccountResponse>(x));
            return new ObjectResult(accountResponses);
        }

    }
}
