﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SoftITOFlix.Data;
using SoftITOFlix.Models;

namespace SoftITOFlix.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly SoftITOFlixContext _context;

        public CategoriesController(SoftITOFlixContext context)
        {
            _context = context;
        }

        // GET: api/Categories
        [HttpGet]
        public ActionResult<List<Category>> GetCategories()
        {
            return _context.Categories.AsNoTracking().ToList();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        [Authorize]
        public ActionResult<Category> GetCategory(short id)
        {
            Category? category = _context.Categories.Find(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(Roles = "ContentAdmin")]
        public void PutCategory(Category category)
        {
            _context.Categories.Update(category);

            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
            }
        }

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(Roles = "ContentAdmin")]
        public short PostCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();

            return category.Id;
        }
    }
}
