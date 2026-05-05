using System;
using System.Collections.Generic;
using System.Text;
using Wallet.Domain.Enums;

namespace Wallet.Application.DTOs.Pocket
{
    public class PocketsInWalletResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public PocketRole Role { get; set; }
        public BalanceSource DefaultBalanceSource { get; set; }
        public bool IsFixedBudget { get; set; }
        public decimal EffectivePocketBalance {  get; set; }
    }
}
