﻿using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository.IRepository
{
    public interface ICategoryRepository :IRepository<Category>
    {
        //Remember to implement some model/class specific methods over its interface

        void Update(Category objCategory);
       
    }
}