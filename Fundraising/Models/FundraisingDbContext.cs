using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Fundraising.Models
{
    public partial class FundraisingDbContext : DbContext
    {
        public FundraisingDbContext()
        {
        }

        public FundraisingDbContext(DbContextOptions<FundraisingDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Answer> Answers { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Following> Followings { get; set; }
        public virtual DbSet<Message> Messages { get; set; }
        public virtual DbSet<News> News { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderState> OrderStates { get; set; }
        public virtual DbSet<Plan> Plans { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<ProductState> ProductStates { get; set; }
        public virtual DbSet<Question> Questions { get; set; }
        public virtual DbSet<Refund> Refunds { get; set; }
        public virtual DbSet<RefundState> RefundStates { get; set; }
        public virtual DbSet<User> Users { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("Server=fundraisingdb.database.windows.net;Database=FundraisingDb;user id=testuser;password=Test0000;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Answer>(entity =>
            {
                entity.ToTable("Answer");

                entity.Property(e => e.AnswerId).HasColumnName("AnswerID");

                entity.Property(e => e.AnswerContent)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.AnswerTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.CommentId)
                    .HasColumnName("CommentID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Comment)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.CommentId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answer_Comment");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answer_User");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.ToTable("Comment");

                entity.Property(e => e.CommentId).HasColumnName("CommentID");

                entity.Property(e => e.CommentContent)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Commenttime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_Product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Comment_User");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.EmployeeId).HasColumnName("EmployeeID");

                entity.Property(e => e.Account)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Following>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.ProductId });

                entity.ToTable("Following");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Followings)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Following_Product");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Followings)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Following_User");
            });

            modelBuilder.Entity<Message>(entity =>
            {
                entity.ToTable("Message");

                entity.Property(e => e.MessageId).HasColumnName("MessageID");

                entity.Property(e => e.Contact)
                    .IsRequired()
                    .HasMaxLength(200)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.FromUserId)
                    .HasColumnName("FromUserID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SentTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.ToUserId)
                    .HasColumnName("ToUserID")
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.FromUser)
                    .WithMany(p => p.MessageFromUsers)
                    .HasForeignKey(d => d.FromUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_User");

                entity.HasOne(d => d.ToUser)
                    .WithMany(p => p.MessageToUsers)
                    .HasForeignKey(d => d.ToUserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_User1");
            });

            modelBuilder.Entity<News>(entity =>
            {
                entity.Property(e => e.NewsId).HasColumnName("NewsID");

                entity.Property(e => e.NewsContent)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.NewsDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.NewsTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.News)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_News_Product");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("Order");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.AddSponsorship).HasDefaultValueSql("('')");

                entity.Property(e => e.MasterCardId)
                    .IsRequired()
                    .HasMaxLength(16)
                    .IsUnicode(false)
                    .HasColumnName("MasterCardID")
                    .HasDefaultValueSql("('')")
                    .IsFixedLength(true);

                entity.Property(e => e.Note)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.OrderDateId)
                    .IsRequired()
                    .HasMaxLength(12)
                    .IsUnicode(false)
                    .HasColumnName("OrderDateID")
                    .HasDefaultValueSql("([dbo].[Order_NextID]())")
                    .IsFixedLength(true);

                entity.Property(e => e.OrderStateId)
                    .HasColumnName("OrderStateID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.PlanId)
                    .HasColumnName("PlanID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PostCode)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PurchaseTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.RecipientAddress)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecipientMail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecipientName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RecipientPhone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')")
                    .IsFixedLength(true);

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.OrderState)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.OrderStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_OrderState");

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.PlanId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_Plan");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Order_User1");
            });

            modelBuilder.Entity<OrderState>(entity =>
            {
                entity.ToTable("OrderState");

                entity.Property(e => e.OrderStateId)
                    .ValueGeneratedNever()
                    .HasColumnName("OrderStateID");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Plan>(entity =>
            {
                entity.ToTable("Plan");

                entity.Property(e => e.PlanId).HasColumnName("PlanID");

                entity.Property(e => e.PlanContent)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PlanPhoto)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PlanPrice).HasDefaultValueSql("('')");

                entity.Property(e => e.PlanTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Plans)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Project_Product");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.ToTable("Product");

                entity.Property(e => e.ProductId).HasColumnName("ProductID");

                entity.Property(e => e.Coverphoto)
                    .IsRequired()
                    .HasColumnName("coverphoto")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Endtime)
                    .HasColumnType("date")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Featured)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PrincipalBankAccount)
                    .IsRequired()
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PrincipalEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PrincipalId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("PrincipalID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PrincipalName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.PrincipalPhone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProductContent)
                    .IsRequired()
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProductStateId)
                    .HasColumnName("ProductStateID")
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.ProductTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ProductVedio)
                    .IsRequired()
                    .HasMaxLength(1000)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Startime)
                    .HasColumnType("date")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.TargetAmount).HasDefaultValueSql("('')");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.ProductState)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.ProductStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_ProductState");
            });

            modelBuilder.Entity<ProductState>(entity =>
            {
                entity.ToTable("ProductState");

                entity.Property(e => e.ProductStateId)
                    .ValueGeneratedNever()
                    .HasColumnName("ProductStateID");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<Question>(entity =>
            {
                entity.ToTable("Question");

                entity.Property(e => e.QuestionId).HasColumnName("QuestionID");

                entity.Property(e => e.ProductId)
                    .HasColumnName("ProductID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.QuestionContent)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.QuestionTitle)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.Questions)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Question_Product");
            });

            modelBuilder.Entity<Refund>(entity =>
            {
                entity.ToTable("Refund");

                entity.Property(e => e.RefundId).HasColumnName("RefundID");

                entity.Property(e => e.OrderId)
                    .HasColumnName("OrderID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RefundResult)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.RefundStateId)
                    .HasColumnName("RefundStateID")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.Refunds)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Refund_Order");

                entity.HasOne(d => d.RefundState)
                    .WithMany(p => p.Refunds)
                    .HasForeignKey(d => d.RefundStateId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Refund_RefundState");
            });

            modelBuilder.Entity<RefundState>(entity =>
            {
                entity.ToTable("RefundState");

                entity.Property(e => e.RefundStateId).HasColumnName("RefundStateID");

                entity.Property(e => e.State)
                    .IsRequired()
                    .HasMaxLength(30)
                    .HasDefaultValueSql("('')");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("User");

                entity.Property(e => e.UserId).HasColumnName("UserID");

                entity.Property(e => e.CreateDate)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserBirthday)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.UserEmail)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserFblink)
                    .IsRequired()
                    .HasMaxLength(999)
                    .HasColumnName("UserFBLink")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserGender)
                    .IsRequired()
                    .HasDefaultValueSql("((1))");

                entity.Property(e => e.UserIntro)
                    .IsRequired()
                    .HasMaxLength(999)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserName)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserPassword)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserPhone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserPhoto)
                    .IsRequired()
                    .HasDefaultValueSql("('')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
