using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SamINV.Models;

namespace SamINV.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly InvoiceContext _context;

        public InvoiceController(InvoiceContext context)
        {
            _context = context;
        }

        // GET: api/Invoice
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Invoice>>> GetInvoices()
        {
            return await _context.Invoices.ToListAsync();
        }
        
        // GET: api/Invoice
        [HttpGet("{id}")]
        public async Task<ActionResult<Invoice>> GetInvoice(long id)
        {
            var invoice = await _context.Invoices.FindAsync(id);

            if (invoice == null)
            {
                return NotFound();
            }

            return invoice;
        }

        // PUT: api/Invoice
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutInvoice(long id, Invoice invoice)
        {
            if (id != invoice.Id)
            {
                return BadRequest();
            }

            _context.Entry(invoice).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!InvoiceExists(id))
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

        // POST: api/Invoice
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Invoice>> PostTodoItem(Invoice invoice)
        {
            _context.Invoices.Add(invoice);
            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetInvoice", new { id = invoice.Id }, invoiceItem);
            return CreatedAtAction(nameof(GetInvoice), new { id = invoice.Id }, invoice);
        }

        // DELETE: api/Invoice
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteInvoice(long id)
        {
            var todoItem = await _context.Invoices.FindAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            _context.Invoices.Remove(todoItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool InvoiceExists(long id)
        {
            return _context.Invoices.Any(e => e.Id == id);
        }
    }
}
