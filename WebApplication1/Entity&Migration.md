# DataBase ��lemleri
## 1. [Entity](#entity) s�n�flar� olu�turulur
## 2. [DBContext](#dbcontext) s�n�f� olu�turulur
## 3. [Migration](#migration) olu�turulur
## 4. [Ek Migration Komutlar�](#migration-komutlar) kullan�l�r

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
- [Required]: Zorunlu alan oldu�unu belirtir
- [StringLength(n)]: String alan�n maksimum uzunlu�unu belirler
- [EmailAddress]: Email format� kontrol� yapar
- [DataType]: Veri tipini belirtir
- [DatabaseGenerated]: ID'nin otomatik artmas�n� sa�lar
- [ForeignKey]: Foreign key ili�kisini belirtir
- [InverseProperty]: �li�kinin ters taraf�n� belirtir

### AYRICA
- [Table("Musteriler")] // Tablo ad�n� �zelle�tirmek i�in
- [Column("CustomerName")] // Kolon ad�n� �zelle�tirmek i�in
- [Timestamp] // Veri de�i�ti�inde g�ncelleme yapar
- [Table("Musteriler", Schema = "dbo")] // �ema belirtmek i�in

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
            // Mevcut ili�kiler
            modelBuilder.Entity<Musteri>()
                .HasOne(m => m.role)
                .WithMany()
                .HasForeignKey("RoleId");

            // Admin-Role ili�kisi
            modelBuilder.Entity<Admin>()
                .HasOne(a => a.role)
                .WithMany()
                .HasForeignKey("RoleId");
        }
    }
}

```
- OnConfiguring metodu ile veritaban� ba�lant�s� yap�l�r.  
` optionsBuilder.UseSqlServer("server=localhost,1439;database=c240;uId=sa;password=Asd123asd.;TrustServerCertificate=True;"); `
- DbSet<Musteri> Musteriler { get; set; } ile tablo tan�mlan�r.
- DbSet<Role> Roles { get; set; } ile tablo tan�mlan�r.
- DbSet<Admin> Admins { get; set; } ile tablo tan�mlan�r.
- "DbContext" s�n�f� i�erisinde "OnModelCreating" metodu ile tablo ili�kileri belirtilir.  
` modelBuilder.Entity<Musteri>()
                .HasOne(m => m.role)
                .WithMany()
                .HasForeignKey("RoleId"); `

- "HasOne" ile bir ili�ki belirtilir.
- "WithMany" ile bir ili�kinin �oklu oldu�u belirtilir.
- "HasForeignKey" ile foreign key belirtilir.
- "RoleId" ile foreign key alan� belirtilir.
- hasOne ve WithMany aras�nda bir ili�ki belirtilmez ise "One to One" ili�ki olu�ur.
- hasOne ve WithMany aras�nda bir ili�ki belirtilirse "One to Many" ili�ki olu�ur.
- many to many ili�kilerde "HasOne" ve "WithMany" aras�nda bir ili�ki belirtilmez.
- hasData ile veritaban�na varsay�lan veri eklenir.  
` modelBuilder.Entity<Role>().HasData(new Role { id = 1, name = "Admin" }); `
# 3. Migration
- Nugetten Microsoft.EntityFrameworkCore.Tools y�klenmeli
- entity ve context s�n�flar�n� olu�turduktan sonra
- "Tools" > "NuGet Package Manager" > "Package Manager Console"
- daha �nce tablo olu�turuldu ise Drop-Database komutu ile veritaban� silinir.  
` Drop-Database `
- Add-Migration InitialCreate komutu ile migration olu�turulur.  
` Add-Migration InitialCreate `
- "Update-Database" komutu ile veritaban�na yans�t�l�r.  
` Update-Database `
- Migration olu�turulduktan sonra "Migrations" klas�r� alt�nda migration dosyas� olu�ur. 

# 4. Migration Komutlar�
- tek tablo migration olu�turulacak ise "Add-Migration MigrationAdi" komutu ile migration olu�turulur.  
` Add-Migration AddAdminEntity `
- "Remove-Migration" komutu ile migration geri al�nabilir.  
` Remove-Migration `
- "Update-Database -Migration:0" komutu ile veritaban� s�f�rlan�r.  
` Update-Database -Migration:0 `
- "Update-Database -Migration:MigrationAdi" komutu ile belirli bir migration'a geri d�n�l�r.  
` Update-Database -Migration:MigrationAdi `
- "Script-Migration" komutu ile SQL scripti al�nabilir.  
` Script-Migration `
- "Update-Database -Verbose" komutu ile detayl� bilgi al�nabilir.  
` Update-Database -Verbose `
- "Update-Database -Context contextadi" komutu ile belirli bir context'e g�re migration yap�labilir.  
` Update-Database -Context DbBaglan `
- "Update-Database -Context contextadi -Migration MigrationAdi" komutu ile belirli bir context'e g�re belirli bir migration yap�labilir.  
` Update-Database -Context DbBaglan -Migration AddAdminEntity `
- "Update-Database -Context contextadi -Migration 0" komutu ile belirli bir context'e g�re belirli bir migration yap�labilir.  
` Update-Database -Context DbBaglan -Migration 0 `
