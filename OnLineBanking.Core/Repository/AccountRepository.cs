using AutoMapper;
using CloudinaryDotNet.Actions;
using Microsoft.EntityFrameworkCore;
using OnLineBanking.Core.Domain;
using OnLineBanking.Core.DTO;
using OnLineBanking.Core.IRepository;
using OnLineBanking.Core.IServices;
using OnLineBanking.Core.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace OnLineBanking.Core.Repository
{
    public class AccountRepository :GenericRepository<Account> ,IBankAccountRepository
    {
        private readonly OnlineBankDBContext _context;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public AccountRepository(OnlineBankDBContext context,IMapper mapper):base(context)
        {
            _context = context;
            _mapper = mapper;
                 
        }

        public async  Task<Response<string>> Authenticate(AuthenticateDto authenticate)
        {
            try
            {
                var account =await _context.accounts.Where(x => x.Account_Number ==
                authenticate.AccountNumber).FirstOrDefaultAsync();
                if (account == null)
                    return new Response<string>
                    {
                        Data = null,
                        Succeeded = false,
                        StatusCode = 404,
                        Message = "Account Not found"
                    };
                if (!VerifyPasswordHash(authenticate.Password, account.PasswordHash, account.PasswordSalt))
                {
                    return Response<string>.Fail("Authentication failed");
                }
                return Response<string>.Success("Banwk authentication Sucessful", account.AccountName);
            }
            catch (Exception ex)
            {

                return Response<string>.Fail($"Authentication failed {ex.Message}");
            }
        }
        private bool VerifyPasswordHash(string paswword, byte[] passwordHash, byte[] passwordSalt)
        {
            if (string.IsNullOrWhiteSpace(paswword))
                throw new ArgumentNullException("password");
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedPasswordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(paswword));
                for (int i = 0; i < computedPasswordHash.Length; i++)
                {
                    if (computedPasswordHash[i] != passwordHash[i])
                        return false;
                }
            }
            return true;
        }
        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var Hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = Hmac.Key;
                passwordHash = Hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
        public async  Task<Response<AccountDto>> Create(CreateAccountDto createAccount)
        {
            try
            {
                if (!createAccount.Password.Equals(createAccount.ConfirmPassword))
                    throw new ArgumentException("Passwords do not match");
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(createAccount.Password, out passwordHash, out passwordSalt);
                var newAccount = _mapper.Map<Account>(createAccount);
                newAccount.PasswordSalt = passwordSalt;
                newAccount.PasswordHash = passwordHash;
                _context.Add(newAccount);
                await _unitOfWork.SaveChanges();
                var createdAccount = _mapper.Map<AccountDto>(newAccount);
                return Response<AccountDto>.Success("Account Created sucessfully", createdAccount);
            }
            catch (Exception ex)
            {

                return Response<AccountDto>.Fail("Account creation failed" + ex.Message);
            }  
        }
        public async  Task UpdateAsync(Account account)
        {
            _context.Entry(account).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

       
    }
}
