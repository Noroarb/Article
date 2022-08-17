using Card.Common;
using Card.Services.Dtos;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Services.Interfaces
{
    public interface ICardsService
    {
        #region Wessam region

        /// <summary>
        /// وسام
        /// يستخدم هذا التابع لتعديل أسعار البطاقات
        /// الأسعار فقط وليس اسم البطاقة
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        bool CardPriceUpdate(CardDto dto);

        /// <summary>
        /// وسام
        /// جلب تفاصيل بطاقة معينة حسب 
        /// Id
        /// </summary>
        /// <param name="cardId"></param>
        /// <returns></returns>
        CardDto GetById(int cardId);

        /// <summary>
        /// وسام
        /// جلب كامل البطاقات مع تفاصيلها
        /// </summary>
        /// <returns></returns>
        List<CardDto> GetAll();


        #endregion

        #region khalil region

        /// <summary>
        /// Add new Card
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        int Add(CardDto dto);

        bool Edit(CardDto dto);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        bool? Delete(int id);

        #endregion
    }
}
