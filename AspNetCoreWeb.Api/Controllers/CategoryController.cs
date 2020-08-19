﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using MediatR;
using AspNetCoreWebApi.Core.Configuration;
using AspNetCoreWebApi.Application.Commands;
using AspNetCoreWebApi.Application.Queries;
using AspNetCoreWebApi.Application.Responses;
using AspNetCoreWebApi.Core.Pagination;

namespace AspNetCoreWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Role.Admin)]
    public class CategoryController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoryController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet("getAll")]
        [ProducesResponseType(typeof(IEnumerable<CategoryResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<CategoryResponse>>> GetAllCategories()
        {
            return Ok(await _mediator.Send(new GetAllCategoriesQuery()));
        }

        [HttpPost("search")]
        [ProducesResponseType(typeof(IPagedList<CategoryResponse>),  StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IPagedList<CategoryResponse>>> Search([FromBody] PageSearchArgs searchArgs)
        {
            return Ok(await _mediator.Send(new SearchCategoriesQuery(searchArgs.Args)));
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(CategoryResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CategoryResponse>> Create([FromBody] CreateCategoryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
