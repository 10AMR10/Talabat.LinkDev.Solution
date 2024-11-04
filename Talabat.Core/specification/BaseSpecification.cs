﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Entities;

namespace Talabat.Core.specification
{
	public class BaseSpecification<T> : ISpecification<T> where T : BaseEntity
	{
		public Expression<Func<T, bool>>? Criteria { get; set; }=null; // x=> x.id==id
		public List<Expression<Func<T, object>>> Includes { get; set; } = new List<Expression<Func<T, object>>>();
		public Expression<Func<T, object>> OrderBy { get; set; } = null;
		public Expression<Func<T, object>> OrderByDesc { get; set; } = null;

		public BaseSpecification()
        {	
			
            //Criteria = null;
            //Includes = new List<Expression<Func<T, object>>>();


        }
        public BaseSpecification(Expression<Func<T, bool>> criteria)
		{
            Criteria=criteria;
            //Includes = new List<Expression<Func<T, object>>>();

		}
	}
}
