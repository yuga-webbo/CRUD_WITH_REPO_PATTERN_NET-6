using AutoMapper;
using Ecommerce.Service.Interface;
using Ecommerce.Repository;
using Ecommerce.Entities.Entity;
using Ecommerce.Repository.Interface;
using Ecommerce.Models.Models;
using Ecommerce.Service.Helpers;

namespace Ecommerce.Service.Service
{
    

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return  _unitOfWork.Users.GetAll();
        }

        public async Task<User> GetById(int id)
        {
            var user =  _unitOfWork.Users.GetById(id);
            if (user == null) throw new KeyNotFoundException("User not found");
            return user;
        }

        public void Create(CreateRequest model)
        {
            // validate
            var data = _unitOfWork.Users.Find(x => x.Email == model.Email);
            if (data != null)
                throw new AppException("User with the email '" + model.Email + "' already exists");

            // map model to new user object
            var user = _mapper.Map<User>(model);

            // hash password
            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // save user
             _unitOfWork.Users.Add(user);
             _unitOfWork.Complete();
        }

        public void Update(int id, UpdateRequest model)
        {
            var user =GetById(id).Result;

            var data = _unitOfWork.Users.Find(x => x.Email == model.Email); 

            // validate
            if (model.Email != user.Email && data!=null)
                throw new AppException("User with the email '" + model.Email + "' already exists");

            // hash password if it was entered
            if (!string.IsNullOrEmpty(model.Password))
                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(model.Password);

            // copy model to user and save
            _mapper.Map(model, user);
            _unitOfWork.Users.Update(user);
            _unitOfWork.Complete();
        }

        public void Delete(int id)
        {
            var user = GetById(id).Result;
            _unitOfWork.Users.Remove(user);
            _unitOfWork.Complete();
        }

    }

}
