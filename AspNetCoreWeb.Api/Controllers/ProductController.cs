﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using MediatR;
using AutoMapper;
using AspNetCoreWebApi.Core.Logging;
using AspNetCoreWebApi.Core.Configuration;
using AspNetCoreWebApi.Core.Pagination;
using AspNetCoreWebApi.Application.Commands;
using AspNetCoreWebApi.Application.Queries;
using AspNetCoreWebApi.Application.Responses;

namespace AspNetCoreWeb.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = Role.Admin)]
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IAppLogger<ProductController> _logger;

        public ProductController(IMediator mediator, IAppLogger<ProductController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("getAll")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetAllProducts()
        {
            return Ok(await _mediator.Send(new GetAllProductsQuery()));
        }

        [HttpPost("search")]
        [ProducesResponseType(typeof(IPagedList<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IPagedList<ProductResponse>>> Search([FromBody] PageSearchArgs searchArgs)
        {
            return Ok(await _mediator.Send(new SearchProductsQuery(searchArgs.Args)));
        }

        [HttpGet("getById/{productId:int}")]        
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductResponse>> GetProductById(int productId)
        {
            return Ok(await _mediator.Send(new GetProductByIdQuery(productId)));
        }

        [HttpGet("getByCode/{productCode}")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByCode(string productCode)
        {
            return Ok(await _mediator.Send(new GetProductsByCodeQuery(productCode)));
        }

        [HttpGet("getByCategoryId/{categoryId:int}")]
        [ProducesResponseType(typeof(IEnumerable<ProductResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<IEnumerable<ProductResponse>>> GetProductsByCategoryId(int categoryId)
        {
            return Ok(await _mediator.Send(new GetProductsByCategoryIdQuery(categoryId)));
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(ProductResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<ProductResponse>> Create([FromBody]CreateProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Update([FromBody] UpdateProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }

        [HttpPut("delete")]
        [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> Delete([FromBody]DeleteProductCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}
