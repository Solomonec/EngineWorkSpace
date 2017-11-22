using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Globalization;
using System.Web.Security;

namespace MetroSupport.Models
{
    public class MetroSupportContext : DbContext
    {
        public MetroSupportContext()
            : base("MetroSupportConnection")
        {

        }

        public DbSet<ItCallRequest> ItCallRequests { get; set; }
        public DbSet<SvyazCallRequest> SvyazCallRequests { get; set; }
        public DbSet<AsppCallRequest> AsppCallRequests { get; set; }
        public DbSet<PaCallRequest> PaCallRequests { get; set; }
        public DbSet<ItCallRequestLog> ItCallRequestLogs { get; set; }
        public DbSet<SvyazCallRequestLog> SvyazCallRequestLogs { get; set; }
        public DbSet<AsppCallRequestLog> AsppCallRequestLogs { get; set; }
        public DbSet<PaCallRequestLog> PaCallRequestLogs { get; set; }
        public DbSet<DeviceModel> DeviceModels { get; set; }
        public DbSet<ItCategory> ItCategories { get; set; }
        public DbSet<SvyazCategory> SvyazCategories { get; set; }
        public DbSet<AsppCategory> AsppCategories { get; set; }
        public DbSet<PaCategory> PaCategories { get; set; }
        public DbSet<CategoryIndexator> CategoryIndexators{ get; set; }
        public DbSet<ModelIndexator> ModelIndexators { get; set; }
        public DbSet<RequestOwner> RequestOwners { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Assigner> Assigners { get; set; }
        public DbSet<AssignBoss> AssignBoss { get; set; }
        public DbSet<TroubleSubject> TroubleSubjects { get; set; }
        public DbSet<Department> Departments { get; set; }
   

    }

    [Table("ItCallRequest")]
    public class ItCallRequest
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid CallRequestId { get; set; }
        public string RequestNumber { get; set; }
        [Required(ErrorMessage = "*")]
        public string TroubleSubject { get; set; }
        public DateTime? Creation { get; set; }
        public DateTime? Time { get; set; }
        public int Status { get; set; }
        public int IsWorkingOn { get; set; }
        public string Comment { get; set; }
        [Required(ErrorMessage = "*")]
        public string InvNumber { get; set; }
        public string DeviceName { get; set; }
        public string SerialNumber { get; set; }
        public string DeviceClass { get; set; }
        public string DeviceType { get; set; }
        public string DeviceModel { get; set; }
        public DateTime? DateInWork { get; set; }
        public string TroubleDepartment { get; set; }
        [Required(ErrorMessage = "*")]
        public string Category { get; set; }
        
        public string NextSubCategoryId { get; set; }
        [Required(ErrorMessage = "*")]
        public string SubCategory1 { get; set; }
   
        public string NextSubCategoryId1 { get; set; }
        [Required(ErrorMessage = "*")]
        public string SubCategory2 { get; set; }
       
        public string NextSubCategoryId2 { get; set; }
        [Required(ErrorMessage = "*")]
        public string SubCategory3 { get; set; }
     
        public string NextSubCategoryId3 { get; set; }
        public string SubCategory4 { get; set; }
        public string NextSubCategoryId4 { get; set; }
        public string SubCategory5 { get; set; }
        public string NextSubCategoryId5 { get; set; }
        public string Model { get; set; }
        public string ModelId { get; set; }
        [Required(ErrorMessage = "*")]
        public string RequestorName { get; set; }
        public string Organization { get; set; }
        public string Department { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string Room { get; set; }
        public string Floor { get; set; }
        public string Location { get; set; }
        [Required(ErrorMessage = "*")]
        public string AssignTo { get; set; }
        [Required(ErrorMessage = "*")]
        public string AssignBoss { get; set; }
        public string AssignSubDepartment { get; set; }
        public string AssignDepartment { get; set; }
        public DateTime? StartDateInWork { get; set; }
        public DateTime? StartTimeInWork { get; set; }
        public DateTime? EndDateInWork { get; set; }
        public DateTime? EndTimeInWork { get; set; }
        public int TotalWorkInDays { get; set; }
        public int TotalWorkInHours { get; set; }
        public int TotalWorkInMinutes { get; set; }
        public string TroubleReason { get; set; }
        public string Prevention { get; set; }
        
        public bool Remote { get; set; }
        
        public string TroubleDescription { get; set; }
     
        public string ResolveDescription { get; set; }

        public ItCallRequestLog ItCallRequestLog { get; set; }

    }

    [Table("AsppCallRequest")]
    public class AsppCallRequest
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid CallRequestId { get; set; }
        public string RequestNumber { get; set; }
        [Required(ErrorMessage = "*")]
        public string TroubleSubject { get; set; }
        public DateTime? Creation { get; set; }
        public DateTime? Time { get; set; }
        public int Status { get; set; }
        public int IsWorkingOn { get; set; }
        public string Comment { get; set; }
        [Required(ErrorMessage = "*")]
        public string InvNumber { get; set; }
        public string DeviceName { get; set; }
        public string SerialNumber { get; set; }
        public string DeviceClass { get; set; }
        public string DeviceType { get; set; }
        public string DeviceModel { get; set; }
        public DateTime? DateInWork { get; set; }
        public string TroubleDepartment { get; set; }
        [Required(ErrorMessage = "*")]
        public string Category { get; set; }
        
        public string NextSubCategoryId { get; set; }
        [Required(ErrorMessage = "*")]
        public string SubCategory1 { get; set; }
        
        public string NextSubCategoryId1 { get; set; }
        [Required(ErrorMessage = "*")]
        public string SubCategory2 { get; set; }
       
        public string NextSubCategoryId2 { get; set; }
        [Required(ErrorMessage = "*")]
        public string SubCategory3 { get; set; }
       
        public string NextSubCategoryId3 { get; set; }
        public string SubCategory4 { get; set; }
        public string NextSubCategoryId4 { get; set; }
        public string SubCategory5 { get; set; }
        public string NextSubCategoryId5 { get; set; }
        public string Model { get; set; }
        public string ModelId { get; set; }
        [Required(ErrorMessage = "*")]
        public string RequestorName { get; set; }
        public string Organization { get; set; }
        public string Department { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string Room { get; set; }
        public string Floor { get; set; }
        public string Location { get; set; }
        [Required(ErrorMessage = "*")]
        public string AssignTo { get; set; }
        [Required(ErrorMessage = "*")]
        public string AssignBoss { get; set; }
        public string AssignSubDepartment { get; set; }
        public string AssignDepartment { get; set; }
        public DateTime? StartDateInWork { get; set; }
        public DateTime? StartTimeInWork { get; set; }
        public DateTime? EndDateInWork { get; set; }
        public DateTime? EndTimeInWork { get; set; }
        public int TotalWorkInDays { get; set; }
        public int TotalWorkInHours { get; set; }
        public int TotalWorkInMinutes { get; set; }
        public string TroubleReason { get; set; }
        public string Prevention { get; set; }
        public bool Remote { get; set; }
        public string TroubleDescription { get; set; }
        public string ResolveDescription { get; set; }

        public AsppCallRequestLog AsppCallRequestLog { get; set; }

    }

    [Table("SvyazCallRequest")]
    public class SvyazCallRequest
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid CallRequestId { get; set; }
        public string RequestNumber { get; set; }
        [Required(ErrorMessage = "*")]
        public string TroubleSubject { get; set; }
        public DateTime? Creation { get; set; }
        public DateTime? Time { get; set; }
        public int Status { get; set; }
        public int IsWorkingOn { get; set; }
        public string Comment { get; set; }
        public string RequestorName { get; set; }
        public string Organization { get; set; }
        public string Department { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string Room { get; set; }
        public string Floor { get; set; }
        public string Location { get; set; }
        public string TroubleDepartment { get; set; }
        [Required(ErrorMessage = "*")]
        public string Category { get; set; }
       
        public string NextSubCategoryId { get; set; }
        [Required(ErrorMessage = "*")]
        public string SubCategory1 { get; set; }
       
        public string NextSubCategoryId1 { get; set; }
        [Required(ErrorMessage = "*")]
        public string SubCategory2 { get; set; }
       
        public string NextSubCategoryId2 { get; set; }
        [Required(ErrorMessage = "*")]
        public string SubCategory3 { get; set; }
       
        public string NextSubCategoryId3 { get; set; }
        public string SubCategory4 { get; set; }
        public string NextSubCategoryId4 { get; set; }
        public string SubCategory5 { get; set; }
        public string NextSubCategoryId5 { get; set; }
        public string Model { get; set; }
        public string ModelId { get; set; }
        [Required(ErrorMessage = "*")]
        public string AssignTo { get; set; }
        [Required(ErrorMessage = "*")]
        public string AssignBoss { get; set; }
        public string AssignSubDepartment { get; set; }
        public string AssignDepartment { get; set; }
        public DateTime? StartDateInWork { get; set; }
        public DateTime? StartTimeInWork { get; set; }
        public DateTime? EndDateInWork { get; set; }
        public DateTime? EndTimeInWork { get; set; }
        public int TotalWorkInDays { get; set; }
        public int TotalWorkInHours { get; set; }
        public int TotalWorkInMinutes { get; set; }
        public string TroubleReason { get; set; }
        public string TroubleDescription { get; set; }
        public string ResolveDescription { get; set; }
        public SvyazCallRequestLog SvyazCallRequestLog { get; set; }

    }

    [Table("PaCallRequest")]
    public class PaCallRequest
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid CallRequestId { get; set; }
        public string RequestNumber { get; set; }
        [Required(ErrorMessage = "*")]
        public string TroubleSubject { get; set; }
        public DateTime? Creation { get; set; }
        public DateTime? Time { get; set; }
        public int Status { get; set; }
        public int IsWorkingOn { get; set; }
        public string Comment { get; set; }
        [Required(ErrorMessage = "*")]
        public string InvNumber { get; set; }
        public string DeviceName { get; set; }
        public string SerialNumber { get; set; }
        public string DeviceClass { get; set; }
        public string DeviceType { get; set; }
        public string DeviceModel { get; set; }
        public DateTime? DateInWork { get; set; }
        public string TroubleDepartment { get; set; }
        [Required(ErrorMessage = "*")]
        public string Category { get; set; }
        [Required(ErrorMessage = "*")]
        public string NextSubCategoryId { get; set; }
        public string SubCategory1 { get; set; }
        public string NextSubCategoryId1 { get; set; }
        public string SubCategory2 { get; set; }
        public string NextSubCategoryId2 { get; set; }
        public string SubCategory3 { get; set; }
        public string NextSubCategoryId3 { get; set; }
        public string SubCategory4 { get; set; }
        public string NextSubCategoryId4 { get; set; }
        public string SubCategory5 { get; set; }
        public string NextSubCategoryId5 { get; set; }
        public string Model { get; set; }
        public string ModelId { get; set; }
        [Required(ErrorMessage = "*")]
        public string RequestorName { get; set; }
        public string Organization { get; set; }
        public string Department { get; set; }
        public string Tel { get; set; }
        public string Address { get; set; }
        public string Room { get; set; }
        public string Floor { get; set; }
        public string Location { get; set; }
        [Required(ErrorMessage = "*")]
        public string AssignTo { get; set; }
        [Required(ErrorMessage = "*")]
        public string AssignBoss { get; set; }
        public string AssignSubDepartment { get; set; }
        public string AssignDepartment { get; set; }
        public DateTime? StartDateInWork { get; set; }
        public DateTime? StartTimeInWork { get; set; }
        public DateTime? EndDateInWork { get; set; }
        public DateTime? EndTimeInWork { get; set; }
        public int TotalWorkInDays { get; set; }
        public int TotalWorkInHours { get; set; }
        public int TotalWorkInMinutes { get; set; }
        public string TroubleReason { get; set; }
        public string Prevention { get; set; }
        public bool Remote { get; set; }
        public string TroubleDescription { get; set; }
        public string ResolveDescription { get; set; }
        public PaCallRequestLog PaCallRequestLog { get; set; }

    }

    [Table("ItCallRequestLog")]
    public class ItCallRequestLog
    {
        [Key]
        [ForeignKey("ItCallRequest")]
        public Guid LogId { get; set; }

        [Required]
        [MaxLength(55)]
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

        public ItCallRequest ItCallRequest { get; set; }

    }

    [Table("AsppCallRequestLog")]
    public class AsppCallRequestLog
    {
        [Key]
        [ForeignKey("AsppCallRequest")]
        public Guid LogId { get; set; }

        [Required]
        [MaxLength(55)]
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

        public AsppCallRequest AsppCallRequest { get; set; }

    }

    [Table("SvyazCallRequestLog")]
    public class SvyazCallRequestLog
    {
        [Key]
        [ForeignKey("SvyazCallRequest")]
        public Guid LogId { get; set; }

        [Required]
        [MaxLength(55)]
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

        public SvyazCallRequest SvyazCallRequest { get; set; }

    }

    [Table("PaCallRequestLog")]
    public class PaCallRequestLog
    {
        [Key]
        [ForeignKey("PaCallRequest")]
        public Guid LogId { get; set; }

        [Required]
        [MaxLength(55)]
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

        public PaCallRequest PaCallRequest { get; set; }

    }

    [Table("ItCategory")]
    public class ItCategory
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50)]
        public string CategoryIndexator { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50)]
        public string CategoryType { get; set; }

        public string NextSubCategory { get; set; }

        public string ModelIndexator { get; set; }

    }
    [Table("SvyazCategory")]
    public class SvyazCategory
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50)]
        public string CategoryIndexator { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50)]
        public string CategoryType { get; set; }

        public string NextSubCategory { get; set; }

        public string ModelIndexator { get; set; }

    }
    [Table("PaCategory")]
    public class PaCategory
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50)]
        public string CategoryIndexator { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50)]
        public string CategoryType { get; set; }

        public string NextSubCategory { get; set; }

        public string ModelIndexator { get; set; }
    }
    [Table("AsppCategory")]
    public class AsppCategory
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid CategoryId { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50)]
        public string CategoryIndexator { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(100)]
        public string CategoryName { get; set; }

        [Required(ErrorMessage = "*")]
        [MaxLength(50)]
        public string CategoryType { get; set; }

        public string NextSubCategory { get; set; }

        public string ModelIndexator { get; set; }
    }

    [Table("DeviceModel")]
    public class DeviceModel
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid ModelId { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(50)]
        public string ModelIndexator { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(70)]
        public string ModelName { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(70)]
        public string Department { get; set; }

    }

    

    [Table("CategoryIndexator")]
    public class CategoryIndexator
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid IndexatorId { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(50)]
        public string CategoryIndexatorName { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(50)]
        public string CategoryType { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(50)]
        public string Department { get; set; }
    }

    [Table("ModelIndexator")]
    public class ModelIndexator
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid IndexatorId { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(50)]
        public string ModelIndexatorName { get; set; }
       
        [Required(ErrorMessage = "*")]
        public string Department { get; set; }
    }


    [Table("TroubleSubject")]
    public class TroubleSubject
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid SubjectId { get; set; }
        [Required(ErrorMessage = "*")]
        public string SubjectName { get; set; }
        [Required(ErrorMessage = "*")]
        public string Department { get; set; }

    }



    [Table("Location")]
    public class Location
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid LocationId { get; set; }
        [Required(ErrorMessage = "*")]
        public string LocationName { get; set; }
    }

     [Table("Department")]
    public class Department
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid DepartmentId { get; set; }
        [Required(ErrorMessage = "*")]
        public string DepartmentName { get; set; }
        public string SubDepartmentName { get; set; }
        [Required(ErrorMessage = "*")]
        public string DepartmentType { get; set; }
    }

    [Table("RequestOwner")]
    public class RequestOwner
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public Guid RequestorId { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "*")]
        public string RequestorName { get; set; }
        [Required(ErrorMessage = "*")]
        public string RequestorAltName { get; set; }
        [Required(ErrorMessage = "*")]
        public string Job { get; set; }
        [Required(ErrorMessage = "*")]
        public string Organization { get; set; }
        [Required(ErrorMessage = "*")]
        public string Department { get; set; }
        public string Address { get; set; }
        public string Room { get; set; }
        public string Floor { get; set; }
        public string Tel { get; set; }
        
    }

        [Table("Assigner")]
        public class Assigner
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public Guid AssignerId { get; set; }
            [Required(ErrorMessage = "*")]
            public string AssignerName { get; set; }
            [Required(ErrorMessage = "*")]
            public string BossName { get; set; }
            [Required(ErrorMessage = "*")]
            public string Organization { get; set; }
            [Required(ErrorMessage = "*")]
            public string Department { get; set; }
           
        }

        [Table("AssignBoss")]
        public class AssignBoss
        {
            [Key]
            [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
            public Guid BossId { get; set; }
            [Required(ErrorMessage = "*")]
            public string BossName { get; set; }
            [Required(ErrorMessage = "*")]
            public string Organization { get; set; }
            [Required(ErrorMessage = "*")]
            public string Department { get; set; }
       
        }

}