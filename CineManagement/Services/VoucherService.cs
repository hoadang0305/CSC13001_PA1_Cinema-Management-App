﻿using CineManagement.Models;
using Microsoft.EntityFrameworkCore;

namespace CineManagement.Services
{
    class VoucherService
    {
        public List<Voucher> GetUnusedVouchersByUserId(int userId)
        {
            using (var context = new CinemaManagementContext())
            {
                List<Voucher> vouchers = context.Vouchers.Where(voucher => voucher.UserId == userId && voucher.IsUsed == false).ToList();

                if (vouchers == null)
                {
                    throw new Exception("No voucher found");
                }
                else
                {
                    return vouchers;
                }
            }
        }

        public bool UpdateVoucherStatus(Voucher voucher)
        {
            using (var _context = new CinemaManagementContext())
            {
                var existingVoucher = _context.Vouchers.FirstOrDefault(x => x.VoucherId == voucher.VoucherId);

                if (existingVoucher != null)
                {
                    existingVoucher.IsUsed = true;

                    try
                    {
                        _context.SaveChanges();
                        return true;
                    }
                    catch (DbUpdateException ex)
                    {
                        throw new Exception("An error occurred while updating the voucher's status.", ex);
                    }
                }
                else
                {
                    throw new Exception("Voucher does not exists.");
                }
            }
        }
    }
}
