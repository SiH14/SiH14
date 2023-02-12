﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Fundraising.Models;

namespace Fundraising.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RefundsController : ControllerBase
    {
        private readonly FundraisingDbContext _context;

        public RefundsController(FundraisingDbContext context)
        {
            _context = context;
        }

        // GET: api/Refunds
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Refund>>> GetRefunds()
        {
            return await _context.Refunds.ToListAsync();
        }

        // GET: api/Refunds/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Refund>> GetRefund(int id)
        {
            var refund = await _context.Refunds.FindAsync(id);

            if (refund == null)
            {
                return NotFound();
            }

            return refund;
        }


        
        // PUT: api/Refunds/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRefund(int id, Refund refund)
        {
            if (id != refund.RefundId)
            {
                return BadRequest();
            }

            _context.Entry(refund).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RefundExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }


        //送出退款申請
        // POST: api/Refunds
        [HttpPost]
        public async Task<ActionResult<Refund>> PostRefund(Refund refund)
        {
            _context.Refunds.Add(refund);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRefund", new { id = refund.RefundId }, refund);
        }

        // DELETE: api/Refunds/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRefund(int id)
        {
            var refund = await _context.Refunds.FindAsync(id);
            if (refund == null)
            {
                return NotFound();
            }

            _context.Refunds.Remove(refund);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RefundExists(int id)
        {
            return _context.Refunds.Any(e => e.RefundId == id);
        }
    }
}
