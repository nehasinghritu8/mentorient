﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using mentorient.Data;
using mentorient.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using SQLitePCL;

namespace mentorient.Controllers
{
    public class TenantsController : Controller
    {
        private ApplicationDbContext _context;

        public TenantsController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var tenants = _context.Tenants.ToList();
            
            return View(tenants);
        }
    }
}