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
        public virtual DbSet<Chatroom> Chatrooms { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<EmployeePosition> EmployeePositions { get; set; }
        public virtual DbSet<EmployeeStatus> EmployeeStatuses { get; set; }
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

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=fundraisingdb.database.windows.net;Database=FundraisingDb;user id=testuser;password=Test0000;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

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
                    .HasConstraintName("FK_Answer_Comment");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Answers)
                    .HasForeignKey(d => d.UserId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Answer_User");
            });

            modelBuilder.Entity<Chatroom>(entity =>
            {
                entity.ToTable("Chatroom");

                entity.Property(e => e.ChatroomId).HasColumnName("ChatroomID");

                entity.Property(e => e.UserId1)
                    .HasColumnName("UserID_1")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.UserId2)
                    .HasColumnName("UserID_2")
                    .HasDefaultValueSql("('')");

                entity.HasOne(d => d.UserId1Navigation)
                    .WithMany(p => p.ChatroomUserId1Navigations)
                    .HasForeignKey(d => d.UserId1)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Chatroom_User2");

                entity.HasOne(d => d.UserId2Navigation)
                    .WithMany(p => p.ChatroomUserId2Navigations)
                    .HasForeignKey(d => d.UserId2)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Chatroom_User3");
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

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("email")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Employeephoto)
                    .IsRequired()
                    .HasDefaultValueSql("('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAZAAAAGQCAYAAACAvzbMAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAACxMAAAsTAQCanBgAACqISURBVHhe7d0JnFxVmffxhizd6fS+Vnd119qdpLs63Q0EAoFIEoKCLDMji8s7IiooElTQQfSDrwP4Im4oyhYBnRlcRhkFVHx90Xl1QEXGd14FUTaFLICsWSBBpauqnznPufd2V5IOSU66lq76ffk8dGervvf07fOvc+6951YJAAAOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQFBhxiWbzch41nxmKjs+LplMRtKm9pb+G/375oNkslnvtdL6P/8vABWCAEHFGddOfze9/csvvyybN2+Sp558QjasW2dLP9+06QXZvn2b/7d2ZKLDhIr+H6gsBAgqwrgOF3by5IZ18o1//op89KIL5Zyz3iFnnPZ3cvJrV8rrVy6X1UcslZWHHmzrWPP58SuWy98cu1Ledtopsubt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')");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Phone)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("phone")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Position).HasDefaultValueSql("('')");

                entity.Property(e => e.Sexy)
                    .IsRequired()
                    .HasColumnName("sexy")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.Status)
                    .HasColumnName("status")
                    .HasDefaultValueSql("((1))");

                entity.HasOne(d => d.PositionNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Position)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_EmployeePosition");

                entity.HasOne(d => d.StatusNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.Status)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Employee_EmployeeStatus");
            });

            modelBuilder.Entity<EmployeePosition>(entity =>
            {
                entity.HasKey(e => e.PositionId);

                entity.ToTable("EmployeePosition");

                entity.Property(e => e.PositionId).ValueGeneratedNever();

                entity.Property(e => e.PositionName)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsFixedLength(true);
            });

            modelBuilder.Entity<EmployeeStatus>(entity =>
            {
                entity.HasKey(e => e.StatusId);

                entity.ToTable("EmployeeStatus");

                entity.Property(e => e.StatusId).ValueGeneratedNever();

                entity.Property(e => e.StatusName)
                    .IsRequired()
                    .HasMaxLength(10)
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

                entity.Property(e => e.ChatroomId)
                    .HasColumnName("ChatroomID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.IsRead).HasColumnName("isRead");

                entity.Property(e => e.MessageContent)
                    .IsRequired()
                    .HasMaxLength(300)
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.ReceiverId)
                    .HasColumnName("ReceiverID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SenderId)
                    .HasColumnName("SenderID")
                    .HasDefaultValueSql("('')");

                entity.Property(e => e.SentTime)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.HasOne(d => d.Chatroom)
                    .WithMany(p => p.Messages)
                    .HasForeignKey(d => d.ChatroomId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_Chatroom");

                entity.HasOne(d => d.Receiver)
                    .WithMany(p => p.MessageReceivers)
                    .HasForeignKey(d => d.ReceiverId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_User1");

                entity.HasOne(d => d.Sender)
                    .WithMany(p => p.MessageSenders)
                    .HasForeignKey(d => d.SenderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Message_User");
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
                    .HasDefaultValueSql("('data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAZAAAAGQCAYAAACAvzbMAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAACxMAAAsTAQCanBgAACqISURBVHhe7d0JnFxVmffxhizd6fS+Vnd119qdpLs63Q0EAoFIEoKCLDMji8s7IiooElTQQfSDrwP4Im4oyhYBnRlcRhkFVHx90Xl1QEXGd14FUTaFLICsWSBBpauqnznPufd2V5IOSU66lq76ffk8dGervvf07fOvc+6951YJAAAOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQAAATggQAIATAgQA4IQAAQA4IUAAAE4IEACAEwIEAOCEAAEAOCFAAABOCBAAgBMCBADghAABADghQFBhxiWbzch41nxmKjs+LplMRtKm9pb+G/375oNkslnvtdL6P/8vABWCAEHFGddOfze9/csvvyybN2+Sp558QjasW2dLP9+06QXZvn2b/7d2ZKLDhIr+H6gsBAgqwrgOF3by5IZ18o1//op89KIL5Zyz3iFnnPZ3cvJrV8rrVy6X1UcslZWHHmzrWPP58SuWy98cu1Ledtopsubt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')");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
