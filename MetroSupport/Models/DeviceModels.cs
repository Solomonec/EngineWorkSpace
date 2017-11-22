using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;


namespace DeviceBase.Models
{
    public class DeviceContext : DbContext
    {
        public DeviceContext() : base("DeviceBaseConnection")
        {
           
        }

        public DbSet<ItDevice> ItDevices { get; set; }
        public DbSet<AsppDevice> AsppDevices { get; set; } 
        public DbSet<PaDevice> PaDevices { get; set; }
        public DbSet<Owner> Owners { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<ItDeviceLog> ItDeviceLogs { get; set; }
        public DbSet<AsppDeviceLog> AsppDeviceLogs { get; set; }
        public DbSet<PaDeviceLog> PaDeviceLogs { get; set; }
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<DeviceClass> DeviceClasses { get; set; }
        
    }

    [Table("ItDevice")]

    public class ItDevice
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid DevItGenId { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(20)]
        [Column("DevInvNum", TypeName = "nvarchar")]
        public string DevInvNum { get; set; }

      
        [MaxLength(20)]
        [Column("DevBuhInvNumber", TypeName = "nvarchar")]
        public string DevBuhInvNumber { get; set; }

      
        [MaxLength(55)]
        [Column("SerialNumber", TypeName = "nvarchar")]
        public string SerialNumber { get; set; }

        
        [MaxLength(50)]
        [Column("DeviceClass", TypeName = "nvarchar")]
        public string DeviceClass { get; set; }

        
        [MaxLength(70)]
        [Column("DeviceType", TypeName = "nvarchar")]
        public string DeviceType { get; set; }

        [MaxLength(100)]
        [Column("DeviceModel", TypeName = "nvarchar")]
        public string DeviceModel { get; set; }

        [Column("DateInWork", TypeName = "nvarchar")]
        public string DateInWork { get; set; }

        public DeviceOwner DeviceOwner { get; set; }

        public ItDeviceLog ItDeviceLog { get; set; }

        [MaxLength(200)]
        [Column("Comment", TypeName = "nvarchar")]
         public string Comment { get; set; }

       
        [MaxLength(60)]
        [Column("Location", TypeName = "nvarchar")]
        public string Location { get; set; }


    }

    [Table("AsppDevice")]

    public class AsppDevice
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid DevAsppGenId { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(20)]
        [Column("DevInvNum", TypeName = "nvarchar")]
        public string DevInvNum { get; set; }


        [MaxLength(20)]
        [Column("DevBuhInvNumber", TypeName = "nvarchar")]
        public string DevBuhInvNumber { get; set; }


        [MaxLength(55)]
        [Column("SerialNumber", TypeName = "nvarchar")]
        public string SerialNumber { get; set; }

     
        [MaxLength(55)]
        [Column("DeviceClass", TypeName = "nvarchar")]
        public string DeviceClass { get; set; }

       
        [MaxLength(70)]
        [Column("DeviceType", TypeName = "nvarchar")]
        public string DeviceType { get; set; }

       
        [MaxLength(100)]
        [Column("DeviceModel", TypeName = "nvarchar")]
        public string DeviceModel { get; set; }

        [Column("DateInWork", TypeName = "nvarchar")]
        public string DateInWork { get; set; }

        public DeviceOwner DeviceOwner { get; set; }

        public AsppDeviceLog AsppDeviceLog { get; set; }

        [MaxLength(200)]
        [Column("Comment", TypeName = "nvarchar")]
        public string Comment { get; set; }

        
        [MaxLength(60)]
        [Column("Location", TypeName = "nvarchar")]
        public string Location { get; set; }


    }

    [Table("PaDevice")]

    public class PaDevice
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid DevPaGenId { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(20)]
        [Column("DevInvNum", TypeName = "nvarchar")]
        public string DevInvNum { get; set; }


        [MaxLength(20)]
        [Column("DevBuhInvNumber", TypeName = "nvarchar")]
        public string DevBuhInvNumber { get; set; }


        [MaxLength(55)]
        [Column("SerialNumber", TypeName = "nvarchar")]
        public string SerialNumber { get; set; }

        
        [MaxLength(55)]
        [Column("DeviceClass", TypeName = "nvarchar")]
        public string DeviceClass { get; set; }

        
        [MaxLength(70)]
        [Column("DeviceType", TypeName = "nvarchar")]
        public string DeviceType { get; set; }

        [MaxLength(70)]
        [Column("DeviceSubType", TypeName = "nvarchar")]
        public string DeviceSubType { get; set; }
        
        [MaxLength(100)]
        [Column("DeviceModel", TypeName = "nvarchar")]
        public string DeviceModel { get; set; }

        [Column("DateInWork", TypeName = "nvarchar")]
        public string DateInWork { get; set; }

        public DeviceOwner DeviceOwner { get; set; }

        public PaDeviceLog PaDeviceLog { get; set; }

        [MaxLength(200)]
        [Column("Comment", TypeName = "nvarchar")]
        public string Comment { get; set; }

        
        [MaxLength(60)]
        [Column("Location", TypeName = "nvarchar")]
        public string Location { get; set; }


    }


    [ComplexType]
    public class DeviceOwner
    {

        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        [Column("FullName", TypeName = "nvarchar")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        [Column("Job", TypeName = "nvarchar")]

        public string Job { get; set; }


        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        [Column("Department", TypeName = "nvarchar")]

        public string Department { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        [Column("Slugba", TypeName = "nvarchar")]
        public string Slugba { get; set; }

     
        [MaxLength(20)]
        [Column("Tel", TypeName = "nvarchar")]
        public string Tel { get; set; }

       
        [MaxLength(150)]
        [Column("Address", TypeName = "nvarchar")]

        public string Address { get; set; }

        [Column("Floor")]
        public string Floor { get; set; }

       
        [Column("Room")]
        public string Room { get; set; }

    }

    [Table("ItDeviceLog")]
    public class ItDeviceLog
    {

        [Key]
        [ForeignKey("ItDevice")]
        public Guid DevItGenId { get; set; }

        [Required]
      
        [Column("Creator", TypeName = "nvarchar")]
        public string Creator { get; set; }

        [Required]
        [Column("CreationDate", TypeName = "datetime")]
        public DateTime CreationDate { get; set; }

        [Required]
       
        [Column("WhoLastUpdate", TypeName = "nvarchar")]
        public string WhoLastUpdate { get; set; }

        [Required]
        [Column("LastUpdateDate", TypeName = "datetime")]
        public DateTime LastUpdateDate { get; set; }

        [Required]
      
        [Column("Log", TypeName = "text")]
        public string LogText { get; set; }

       public ItDevice ItDevice { get; set; }

    }

    [Table("AsppDeviceLog")]
    public class AsppDeviceLog
    {

        [Key]
        [ForeignKey("AsppDevice")]
        public Guid DevAsppGenId { get; set; }

        [Required]
       
        [Column("Creator", TypeName = "nvarchar")]
        public string Creator { get; set; }

        [Required]
       
        [Column("CreationDate", TypeName = "datetime")]
        public DateTime CreationDate { get; set; }

        [Required]
        
        [Column("WhoLastUpdate", TypeName = "nvarchar")]
        public string WhoLastUpdate { get; set; }

        [Required]
        
        [Column("LastUpdateDate", TypeName = "datetime")]
        public DateTime LastUpdateDate { get; set; }

        [Required]
      
        [Column("Log", TypeName = "text")]
        public string LogText { get; set; }

        public AsppDevice AsppDevice { get; set; }

    }

    [Table("PaDeviceLog")]
    public class PaDeviceLog
    {

        [Key]
        [ForeignKey("PaDevice")]
        public Guid DevPaGenId { get; set; }

        [Required]
        [MaxLength(20)]
        [Column("Creator", TypeName = "nvarchar")]
        public string Creator { get; set; }

        [Required]
       
        [Column("CreationDate", TypeName = "datetime")]
        public DateTime CreationDate { get; set; }

        [Required]
        [MaxLength(55)]
        [Column("WhoLastUpdate", TypeName = "nvarchar")]
        public string WhoLastUpdate { get; set; }

        [Required]
        
        [Column("LastUpdateDate", TypeName = "datetime")]
        public DateTime LastUpdateDate { get; set; }

        [Required]
       
        [Column("Log", TypeName = "text")]
        public string LogText { get; set; }

        public PaDevice PaDevice { get; set; }

    }

    [Table("Owner")]
    public class Owner
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid OwnerId { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        [Column("Name", TypeName = "nvarchar")]
        public string Name { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        [Column("FullName", TypeName = "nvarchar")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        [Column("Job", TypeName = "nvarchar")]

        public string Job { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        [Column("Slugba", TypeName = "nvarchar")]
        public string Slugba { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        [Column("Department", TypeName = "nvarchar")]
        public string Department { get; set; }

        [MaxLength(100)]
        [Column("Address", TypeName = "nvarchar")]
        public string Address { get; set; }

        [MaxLength(50)]
        [Column("Room", TypeName = "nvarchar")]
        public string Room { get; set; }


        [MaxLength(50)]
        [Column("Floor")]
        public string Floor { get; set; }


        [MaxLength(30)]
        [Column("Tel", TypeName = "nvarchar")]
        public string Tel { get; set; }

       

        
    }

   
    [Table("DeviceType")]
    public class DeviceType
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        
        [MaxLength(100)]
        [Column("DeviceSubTypeName", TypeName = "nvarchar")]
        public string DeviceSubTypeName { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        [Column("DeviceTypeName", TypeName = "nvarchar")]
        public string DeviceTypeName { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        [Column("DeviceClassName", TypeName = "nvarchar")]
        public string DeviceClassName { get; set; }

     
    }

    [Table("DeviceClass")]
    public class DeviceClass
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        [Column("DeviceClassName", TypeName = "nvarchar")]
        public string DeviceClassName { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50)]
        [Column("Department", TypeName = "nvarchar")]
        public string Department { get; set; }
        
       
    }
   
    [Table("Location")]
    public class Location
    {

        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid LocationId { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50)]
        [Column("LocationName", TypeName = "nvarchar")]
        public string LocationName { get; set; }
    }
}
