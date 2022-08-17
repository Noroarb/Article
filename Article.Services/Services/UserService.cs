using AutoMapper;
using Article.Common;
using Article.Domain;
using Article.Domain.Entities;
using Article.Services.Dtos;
using Article.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Article.Services.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public Guid Add(UserDto dto)
        {
            var model = Mapper.Map<UserDto, User>(dto);
            _unitOfWork.UserRepository.Add(model);
            _unitOfWork.SaveChanges();
            return model.UserId;
        }



        public bool Edit(UserDto dto)
        {
            //User user = Mapper.Map<UserDto, User>(dto);
            var user_ = _unitOfWork.UserRepository.FindBy(m => m.UserId == dto.UserId);
            if (user_.Any())
            {
                var user = user_.FirstOrDefault();
               // user.UserName = dto.UserName;
              //  user.Email = dto.Email;
                user.PhoneNumber = dto.PhoneNumber;
              //  user.Location = dto.Location;
                user.FullName = dto.FullName;

                _unitOfWork.UserRepository.Update(user);
                _unitOfWork.SaveChanges();
                return true;
            }

            return false;
        }

        public bool Delete(Guid id)
        {
            User user = _unitOfWork.UserRepository.FindById(id);
            if (user == null)
                return false;
            _unitOfWork.UserRepository.Remove(user);
            _unitOfWork.SaveChanges();
            return true;
        }

        public UserDto GetById(Guid id)
        {
            var model = _unitOfWork.UserRepository.FindById(id);
            return Mapper.Map<User, UserDto>(model);
        }

        public UserDto GetByUserName(string Username)
        {
            var model = _unitOfWork.UserRepository.FindBy(m => m.UserName == Username);
            if (model.Any())
                return Mapper.Map<User, UserDto>(model.FirstOrDefault());
            else
                return null;
        }



        public List<UserDto> GetAll()
        {
            return Mapper.Map<List<User>, List<UserDto>>(_unitOfWork.UserRepository.GetAll());
        }
        public bool HasRole(Guid id, String role)
        {
            return GetById(id).Role == role;
        }
        public bool Exists(Guid id)
        {
            return GetById(id) == null ? false : true;
        }


        public List<UserDto> GetUsersByRole(string role)
        {
            // return GetAll().AsEnumerable().Where(u => HasRole(u.UserId, role)).ToList();
            return Mapper.Map<List<User>, List<UserDto>>(_unitOfWork.UserRepository.FindBy(m => m.Roles.Where(q => q.Name == role).Any()));
        }
        

        public bool IsEmailUnique(string email)
        {
            return _unitOfWork.UserRepository.FindByEmail(email.ToLower()) == null;
        }
        public bool IsUserNameUnique(string UserName)
        {
            return !_unitOfWork.UserRepository.FindBy(m => m.UserName == UserName).Any();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool BlockUserd(Guid userId)
        {
            var models_ = _unitOfWork.UserRepository.FindBy(m => m.UserId == userId);
            if (models_.Any())
            {
                var model = models_.FirstOrDefault();
               
                model.IsActivated = false;
                _unitOfWork.UserRepository.Update(model);
                _unitOfWork.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool UnBlockUserd(Guid userId)
        {
            var models_ = _unitOfWork.UserRepository.FindBy(m => m.UserId == userId);
            if (models_.Any())
            {
                var model = models_.FirstOrDefault();

                model.IsActivated = true;
                _unitOfWork.UserRepository.Update(model);
                _unitOfWork.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ChangeUserToWriter(Guid userId)
        {
            var models_ = _unitOfWork.UserRepository.FindBy(m => m.UserId == userId);
            if (models_.Any())
            {
                var model = models_.FirstOrDefault();

                var roles_ = model.Roles;
                if (roles_.Any())
                {
                    var role_ = roles_.FirstOrDefault();
                    model.Roles.Remove(role_);
                   
                    var modelRole = _unitOfWork.RoleRepository.FindById(Guid.Parse("eddddddd-555d-40ff-85d5-8342ebc5f32c"));
                    model.Roles.Add(modelRole);
                }
                
             
                _unitOfWork.UserRepository.Update(model);
                _unitOfWork.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public bool ChangeUserToSupervisorRole(Guid userId)
        {
            var models_ = _unitOfWork.UserRepository.FindBy(m => m.UserId == userId);
            if (models_.Any())
            {
                var model = models_.FirstOrDefault();

                var roles_ = model.Roles;
                if (roles_.Any())
                {
                    var role_ = roles_.FirstOrDefault();
                    model.Roles.Remove(role_);
                    var modelRole = _unitOfWork.RoleRepository.FindById(Guid.Parse("cccccccc-555d-40ff-85d5-8342ebc5f32c"));
                    model.Roles.Add(modelRole);
                  
                }
                _unitOfWork.UserRepository.Update(model);
                _unitOfWork.SaveChanges();
                return true;
            }
            return false;
        }


        

    }
}
