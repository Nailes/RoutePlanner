using Microsoft.EntityFrameworkCore;

namespace RoutePlanner
{
    public partial class RouteplannerContext : DbContext
    {
        public RouteplannerContext()
        {
        }

        public RouteplannerContext(DbContextOptions<RouteplannerContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Comments> Comments { get; set; }
        public virtual DbSet<Routes> Routes { get; set; }
        public virtual DbSet<Stage> Stage { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Comments>(entity =>
            {
                entity.HasKey(e => e.IdComments);

                entity.ToTable("comments", "routeplanner");

                entity.HasIndex(e => e.IdStage)
                    .HasName("id_stage");

                entity.Property(e => e.IdComments)
                    .HasColumnName("id_comments")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateNote)
                    .HasColumnName("date_note")
                    .HasColumnType("date");

                entity.Property(e => e.IdStage)
                    .HasColumnName("id_stage")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Note)
                    .HasColumnName("note")
                    .HasMaxLength(1000)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdStageNavigation)
                    .WithMany(p => p.Comments)
                    .HasForeignKey(d => d.IdStage)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("comments_ibfk_1");
            });

            modelBuilder.Entity<Routes>(entity =>
            {
                entity.HasKey(e => e.IdRoutes);

                entity.ToTable("routes", "routeplanner");

                entity.HasIndex(e => e.IdUser)
                    .HasName("id_user");

                entity.Property(e => e.IdRoutes)
                    .HasColumnName("id_routes")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateRoutes)
                    .HasColumnName("date_routes")
                    .HasColumnType("date");

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Title)
                    .IsRequired()
                    .HasColumnName("title")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdUserNavigation)
                    .WithMany(p => p.Routes)
                    .HasForeignKey(d => d.IdUser)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("routes_ibfk_1");
            });

            modelBuilder.Entity<Stage>(entity =>
            {
                entity.HasKey(e => e.IdStage);

                entity.ToTable("stage", "routeplanner");

                entity.HasIndex(e => e.IdRoutes)
                    .HasName("id_routes");

                entity.Property(e => e.IdStage)
                    .HasColumnName("id_stage")
                    .HasColumnType("int(11)");

                entity.Property(e => e.DateStage)
                    .HasColumnName("date_stage")
                    .HasColumnType("date");

                entity.Property(e => e.IdRoutes)
                    .HasColumnName("id_routes")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Place)
                    .IsRequired()
                    .HasColumnName("place")
                    .HasMaxLength(200)
                    .IsUnicode(false);

                entity.HasOne(d => d.IdRoutesNavigation)
                    .WithMany(p => p.Stage)
                    .HasForeignKey(d => d.IdRoutes)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("stage_ibfk_1");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.IdUser);

                entity.ToTable("users", "routeplanner");

                entity.HasIndex(e => e.Login)
                    .HasName("login")
                    .IsUnique();

                entity.Property(e => e.IdUser)
                    .HasColumnName("id_user")
                    .HasColumnType("int(11)");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasColumnName("email")
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnName("login")
                    .HasMaxLength(30)
                    .IsUnicode(false);

                entity.Property(e => e.Pass)
                    .IsRequired()
                    .HasColumnName("pass")
                    .HasMaxLength(30)
                    .IsUnicode(false);
            });
        }
    }
}
