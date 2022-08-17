using AutoMapper;
using Card.Domain;
using Card.Common;
using Card.Domain.Entities;
using Card.Services.Dtos;
using Card.Services.Interfaces;
using Card.Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Runtime.InteropServices;
using System.Web.UI;
using System.Web.Http.Routing;
using System.Web.Http;
using PagedList;

namespace Card.Services.Services
{
   /// <summary>
   /// التوابع التي يستخدمها وسام ضمن هذه الصفحة سيتم كتابة كلمة
   /// وسام
   /// فوقها
   /// 
   /// </summary>
    public class CardsService : ICardsService
    {

        private readonly IUnitOfWork _unitOfWork;
        public CardsService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }

        #region Wessam region

        /// <summary>
        /// وسام
        /// يستخدم هذا التابع لتعديل أسعار البطاقات
        /// الأسعار فقط وليس اسم البطاقة
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public bool CardPriceUpdate(CardDto dto)
        {
            var model1s = _unitOfWork.CardsRepository.FindBy(s => s.Id == dto.Id);
            if (model1s.Any())
            {
                var model1 = model1s.FirstOrDefault();
                model1.simpleUser_Price = dto.simpleUser_Price;
                model1.VIPUser_Price = dto.VIPUser_Price;
                model1.TimeAuto = dto.TimeAuto;

                _unitOfWork.CardsRepository.Update(model1);
                _unitOfWork.SaveChanges();
                return true;
            }
            else
                return false;

        }

        /// <summary>
        /// وسام
        /// جلب تفاصيل بطاقة معينة حسب 
        /// Id
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        public CardDto GetById(int cardId)
        {
            var model2 = _unitOfWork.CardsRepository.FindBy(m => m.Id == cardId);

            // var model2 = model1.Where();
            if (model2.Any())
            {
                var model = model2.FirstOrDefault();
                
                return  Mapper.Map<Cards, CardDto >(model); 
            }
            else
                return null;
        }

        /// <summary>
        /// وسام
        /// جلب كامل البطاقات مع تفاصيلها
        /// </summary>
        /// <returns></returns>
        public List<CardDto> GetAll()
        {
            var model2 = _unitOfWork.CardsRepository.GetAll().OrderBy(m=>m.Sort).ToList();
            return Mapper.Map<List<Cards>, List<CardDto>>(model2);
           
        }


        #endregion



        #region khalil region

        /// <summary>
        /// Add new Card
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public int Add(CardDto dto)
        {
              
            var model = Mapper.Map<CardDto, Cards>(dto);
            _unitOfWork.CardsRepository.Add(model);
            _unitOfWork.SaveChanges();
            return model.Id;
           
        }

        public bool Edit(CardDto dto)
        {
           
                
            Cards model1 = _unitOfWork.CardsRepository.FindSingleBy(s => s.Id == dto.Id);
            
            model1.Name = dto.Name;
            model1.simpleUser_Price = dto.simpleUser_Price;
            model1.VIPUser_Price = dto.VIPUser_Price;
            model1.TimeAuto = dto.TimeAuto;
            model1.Sort = dto.Sort;

            _unitOfWork.CardsRepository.Update(model1);
            _unitOfWork.SaveChanges();
            return true;
            
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool? Delete(int id)
        {
           
            Cards cat = _unitOfWork.CardsRepository.FindBy(m=>m.Id==id).FirstOrDefault();
            if (cat == null)
            { return false; }
            else
            if (cat.Orders_Cards.Any())
            { return null; }

            _unitOfWork.CardsRepository.Remove(cat);
            _unitOfWork.SaveChanges();
            return true;
                
        
        }

        #endregion
        
    }
}
