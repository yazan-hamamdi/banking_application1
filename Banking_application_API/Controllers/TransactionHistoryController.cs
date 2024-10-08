﻿using Banking_application_Business.DTOs;
using Banking_application_Business.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Banking_application_API.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionHistoryController : ControllerBase
    {
        private readonly ITransactionHistoryService _transactionHistoryService;
        public TransactionHistoryController(ITransactionHistoryService transactionHistoryService)
        {
            _transactionHistoryService = transactionHistoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTransactionHistories()
        {
            var transactionHistories = await _transactionHistoryService.GetAllAsync();
            return Ok(transactionHistories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetTransactionHistoryById(int id)
        {
            var transactionHistory = await _transactionHistoryService.GetByIdAsync(id);
            if (transactionHistory == null)
                return NotFound();

            return Ok(transactionHistory);
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransactionHistory(TransWithId transactionHistory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var createdTransactionHistory = await _transactionHistoryService.AddAsync(transactionHistory);
            return Ok(UpdateTransactionHistory);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateTransactionHistory(int id,TransWithId transactionHistory)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var updatedTransactionHistory = await _transactionHistoryService.UpdateAsync(transactionHistory, id);
            if (updatedTransactionHistory == null)
                return NotFound();

            return Ok(updatedTransactionHistory);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionHistory(int id)
        {
            await _transactionHistoryService.DeleteAsync(id);
            return NoContent();
        }
    }
}
