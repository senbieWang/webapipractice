﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using WebApiStandardTest.MyAttribute;
using WebApiStandardTest.RequestModelValidation;
using WebApiStandardTest.V2.Models;

//https://www.strathweb.com/2016/09/required-query-string-parameters-in-asp-net-core-mvc/   query parameter check
//https://www.strathweb.com/2017/06/using-iactionconstraints-in-asp-net-core-mvc/   // rout parameter check
//https://docs.microsoft.com/en-us/aspnet/core/mvc/controllers/filters?view=aspnetcore-2.1  filter

namespace WebApiStandardTest.V2.Controllers
{

    [Produces("application/json")]      
    [ApiVersion("2.0")]
    [Route("bims/v{version:apiVersion}/subdatas")]
    [ApiController]
    public class SubdatasController : ControllerBase
    {
        /// <summary>
        /// 获取所有项目子集
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetSubDatas()
        {
            return new string[] { "value1", "value2" };
        }

        /// <summary>
        /// 创建一个项目子集
        /// </summary>
        /// <param name="subdata"></param>
        /// <param name="subdatename">项目子集名称</param>
        [Route("{subdatename}")]
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        
        public void CreateSubData( Subdata subdata,string subdatename)
        {

        }

        /// <summary>
        /// 删除特定的数据子集
        /// </summary>
        /// <param name="subdataid">项目子集名称</param>
        [Route("{subdataid:guid}")]
        [HttpDelete()]
        public void DeleteSubData(string subdataid)
        {

        }

        /// <summary>
        /// 获取特定的项目子集
        /// </summary>
        /// <param name="subdataid">项目子集名称</param>
        /// <returns></returns>

        //[ConstrainedRoute("{subdataid:guid}", typeof(AcceptSubdataIDActionConstraint), "21355")]
        //[RequireHeader("subdataid")]  

        [Route("{subdataid:guid}")]  
        [HttpGet()]     
        public ActionResult<string> GetSubDataByID([SubdataIdConstraint]string subdataid)
        {
            return "value";
        }

        /// <summary>
        /// 给特定的项目子集上传/更新数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subdatename">项目子集名称</param>
        [Route("{subdataid:guid}/bimsdata")]
        [HttpPost]
        public void CreateBimsDataforSubdata( string value,[SubdataNameConstraint] string subdatename)
        {

        }

        /// <summary>
        /// 给特定的项目子集创建组织关系
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="orgname">组织关系名</param>
        /// <returns></returns>
        [Route("{subdataid:guid}/organizations/{orgname}")]
        [HttpPost()]
        public ActionResult<string> CreateOrganizations(string value, string subdataid, string orgname)
        {
            return " Organizations";
        }

        /// <summary>
        /// 获取特定项目子集的特定名称的组织关系数据
        /// </summary>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="orgname">组织关系名</param>
        /// <returns></returns>
        [Route("{subdataid:guid}/organizations/{orgname}")]
        [HttpGet()]
        public ActionResult<string> GetOrganizations(string subdataid, string orgname)
        {
            return "Organizations";
        }

        /// <summary>
        /// 给特定的项目子集的特定的组织关系添加组织关系数据
        /// </summary>
        /// <param name="value"></param>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="orgname">组织关系名</param>
        /// <returns></returns>
        [Route("{subdataid:guid}/organizations/{orgname}/organizationdata")]
        [HttpPost()]
        public ActionResult<string> CreateOrganizationdataFororganization(string value, string subdataid, string orgname)
        {
            return " Organizations";
        }

        /// <summary>
        /// 获取特定项目子集的属性集
        /// </summary>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="versionNo">数据版本号</param>
        /// <returns></returns>
        [Route("{subdataid:guid}/bimsobjects")]
        [HttpGet()]
        public ActionResult<string> GetAttrbuties(string subdataid , int versionNo )
        {
            return "bimsobjects";
        }

        /// <summary>
        /// 获取特定项目子集特定构件的属性，支持过滤查询
        /// </summary>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="bimsobjectid">构件ID</param>
        ///  <param name="versionNo">数据版本号，默认为当前最新版本</param>
        /// <param name="query">查询条件，非必须</param>
        /// <returns></returns>
        [Route("{subdataid:guid}/bimsobjects/{bimsobjectid}")]
        [HttpGet()]
        public ActionResult<string> GetAttrbutie( string subdataid, string bimsobjectid, int versionNo, string query)
        {
            return "bimsobjects";
        }

        /// <summary>
        /// 获取特定项目子集的几何集
        /// </summary>
        /// <param name="subdataid">项目子集ID</param> 
        /// <param name="versionNo">版本号</param>
        /// <returns></returns>
        [Route("{subdataid:guid}/geomtrys")]
        [HttpGet()]
        public ActionResult<string> GetGeomtrys( string subdataid, int versionNo)
        {
            return "geomtrys";
        }

        /// <summary>
        /// 获取特定项目子集的材质集
        /// </summary>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="versionNo">版本号</param>
        /// <returns></returns>
        [Route("{subdataid:guid}/materials")]
        [HttpGet()]
        public ActionResult<string> GetMaterials(string subdataid, int versionNo)
        {
            return "materials";
        }

        /// <summary>
        /// 获取特定项目子集的贴图集
        /// </summary>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="versionNo">版本号</param>
        /// <returns></returns>
        [Route("{subdataid:guid}/textures")]
        [HttpGet()]
        public ActionResult<string> GetTextures(string subdataid, int versionNo)
        {
            return "textures";
        }

        /// <summary>
        /// 获取特定项目子集的管线管件集
        /// </summary>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="versionNo">版本号</param>
        /// <returns></returns>
        [Route("{subdataid:guid}/pipes")]
        [HttpGet()]
        public ActionResult<string> GetPipes(string subdataid, int versionNo)
        {
            return "pipes";
        }

        /// <summary>
        /// 获取特定项目子集的空间集
        /// </summary>
        /// <param name="subdataid">项目子集ID</param>
        /// <param name="versionNo">版本号</param>
        /// <returns></returns>
        [Route("{subdataid:guid}/spaces")]
        [HttpGet()]
        public ActionResult<string> GetSpaces(string subdataid,int versionNo)
        {
            return "spaces";
        }
    }
}
