﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Card.Domain.Entities
{
    public class Cards
    {

        public int Id { get; set; }

        public int Sort { get; set; }
        /// <summary>
        /// نوع البطاقة
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// سعر البطاقة للمستخدم العادي
        /// </summary>
        public decimal? simpleUser_Price { get; set; }
        /// <summary>
        /// سعر البطاقة لمستخدم الجملة
        /// VIP
        /// </summary>
        public decimal? VIPUser_Price { get; set; }

        /// <summary>
        /// الزمن اللازم للتحول من حالة
        /// Pending 
        /// الى حالة
        /// Verified
        /// </summary>
        public int? TimeAuto { get; set; }

        public virtual ICollection<OrdersCards> Orders_Cards { set; get; }
        public virtual ICollection<CardPriceHistory> CardPriceHistory { set; get; }

    }
}
