using ZooDatabaseImplement.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ZooDatabaseImplement
{
    public class ZooDatabase : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured == false)
            {
                optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-9BTVFQR\SQLEXPRESS;Initial Catalog=ZooDatabase;Integrated Security=True;MultipleActiveResultSets=True;");
            }
            base.OnConfiguring(optionsBuilder);
        }
        public virtual DbSet<Worker> Workers { set; get; }
        public virtual DbSet<Client> Clients { set; get; }
        public virtual DbSet<TicketClient> TicketClients { set; get; }
        public virtual DbSet<Ticket> Tickets { set; get; }
    }
}
