# DataBase Ýþlemleri
## 1. [Entity](#entity) sýnýflarý oluþturulur
## 2. [DBContext](#dbcontext) sýnýfý oluþturulur
## 3. [Migration](#migration) oluþturulur
## 4. [Ek Migration Komutlarý](#migration-komutlar) kullanýlýr

# 1. Entity 
```
public class Musteri
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long id { get; set; }

    [Required]
    [StringLength(50)]
    public string name { get; set; }

    [Required]
    [StringLength(50)]
    public string surname { get; set; }

    [Required]
    [EmailAddress]
    [StringLength(100)]
    public string email { get; set; }

    [Required]
    [StringLength(100)]
    [DataType(DataType.Password)]
    public string password { get; set; }

    [ForeignKey("RoleId")]
    public Role role { get; set; }
}
```

- [Key]: Primary key belirtir
- [Required]: Zorunlu alan olduðunu belirtir
- [StringLength(n)]: String alanýn maksimum uzunluðunu belirler
- [EmailAddress]: Email formatý kontrolü yapar
- [DataType]: Veri tipini belirtir
- [DatabaseGenerated]: ID'nin otomatik artmasýný saðlar
- [ForeignKey]: Foreign key iliþkisini belirtir
- [InverseProperty]: Ýliþkinin ters tarafýný belirtir

### AYRICA
- [Table("Musteriler")] // Tablo adýný özelleþtirmek için
- [Column("CustomerName")] // Kolon adýný özelleþtirmek için
- [Timestamp] // Veri deðiþtiðinde güncelleme yapar
- [Table("Musteriler", Schema = "dbo")] // Þema belirtmek için

# 2.DBContext
```
using DBConnectProject.entity;
using entityframeworkdeneme1.entity;
using Microsoft.EntityFrameworkCore;

namespace DBConnectProject.context
{
    public class DbBaglan : DbContext
    {
        public DbSet<Musteri> Musteriler { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Admin> Admins { get; set; }  
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(
                   "server=localhost,1439;" +
                   "database=c240;" +
                   "uId=sa;" +
                   "password=Asd123asd.;" +
                   "TrustServerCertificate=True;"
               );
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Mevcut iliþkiler
            modelBuilder.Entity<Musteri>()
                .HasOne(m => m.role)
                .WithMany()
                .HasForeignKey("RoleId");

            // Admin-Role iliþkisi
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.role)
                .WithMany()
                .HasForeignKey("RoleId");
        }
    }
}

```
- OnConfiguring metodu ile veritabaný baðlantýsý yapýlýr.  
` optionsBuilder.UseSqlServer("server=localhost,1439;database=c240;uId=sa;password=Asd123asd.;TrustServerCertificate=True;"); `
- DbSet<Musteri> Musteriler { get; set; } ile tablo tanýmlanýr.
- DbSet<Role> Roles { get; set; } ile tablo tanýmlanýr.
- DbSet<Admin> Admins { get; set; } ile tablo tanýmlanýr.
- "DbContext" sýnýfý içerisinde "OnModelCreating" metodu ile tablo iliþkileri belirtilir.  
` modelBuilder.Entity<Musteri>()
                .HasOne(m => m.role)
                .WithMany()
                .HasForeignKey("RoleId"); `

- "HasOne" ile bir iliþki belirtilir.
- "WithMany" ile bir iliþkinin çoklu olduðu belirtilir.
- "HasForeignKey" ile foreign key belirtilir.
- "RoleId" ile foreign key alaný belirtilir.
- hasOne ve WithMany arasýnda bir iliþki belirtilmez ise "One to One" iliþki oluþur.
- hasOne ve WithMany arasýnda bir iliþki belirtilirse "One to Many" iliþki oluþur.
- many to many iliþkilerde "HasOne" ve "WithMany" arasýnda bir iliþki belirtilmez.
- hasData ile veritabanýna varsayýlan veri eklenir.  
` modelBuilder.Entity<Role>().HasData(new Role { id = 1, name = "Admin" }); `
# 3. Migration
- Nugetten Microsoft.EntityFrameworkCore.Tools yüklenmeli
- entity ve context sýnýflarýný oluþturduktan sonra
- "Tools" > "NuGet Package Manager" > "Package Manager Console"
- daha önce tablo oluþturuldu ise Drop-Database komutu ile veritabaný silinir.  
` Drop-Database `
- Add-Migration InitialCreate komutu ile migration oluþturulur.  
` Add-Migration InitialCreate `
- "Update-Database" komutu ile veritabanýna yansýtýlýr.  
` Update-Database `
- Migration oluþturulduktan sonra "Migrations" klasörü altýnda migration dosyasý oluþur. 

# 4. Migration Komutlarý
- tek tablo migration oluþturulacak ise "Add-Migration MigrationAdi" komutu ile migration oluþturulur.  
` Add-Migration AddAdminEntity `
- "Remove-Migration" komutu ile migration geri alýnabilir.  
` Remove-Migration `
- "Update-Database -Migration:0" komutu ile veritabaný sýfýrlanýr.  
` Update-Database -Migration:0 `
- "Update-Database -Migration:MigrationAdi" komutu ile belirli bir migration'a geri dönülür.  
` Update-Database -Migration:MigrationAdi `
- "Script-Migration" komutu ile SQL scripti alýnabilir.  
` Script-Migration `
- "Update-Database -Verbose" komutu ile detaylý bilgi alýnabilir.  
` Update-Database -Verbose `
- "Update-Database -Context contextadi" komutu ile belirli bir context'e göre migration yapýlabilir.  
` Update-Database -Context DbBaglan `
- "Update-Database -Context contextadi -Migration MigrationAdi" komutu ile belirli bir context'e göre belirli bir migration yapýlabilir.  
` Update-Database -Context DbBaglan -Migration AddAdminEntity `
- "Update-Database -Context contextadi -Migration 0" komutu ile belirli bir context'e göre belirli bir migration yapýlabilir.  
` Update-Database -Context DbBaglan -Migration 0 `
