﻿using AutoMapper;
using CleanArchitecture.Application.Common.Interfaces;
using CleanArchitecture.Infrastructure.Data;
using Moq;
using NUnit.Framework;
using Microsoft.Extensions.Configuration;
using CleanArchitecture.Application.Common.Mappings;

namespace CleanArchitecture.Application.UnitTests;

[SetUpFixture]
public class BaseTestFixture
{
    protected Mock<IApplicationDbContext> _dbContextMock;
    protected ApplicationDbContext _applicationDbContextMock;
    protected AutoMapper.IConfigurationProvider _configurationprovider;
    protected IConfiguration _configuration;
    protected IMapper _mapper;

    public BaseTestFixture()
    {
        _configurationprovider = new MapperConfiguration(config =>
                                    config.AddProfile<MappingProfile>());
        _mapper = _configurationprovider.CreateMapper();
    }

    [OneTimeSetUp]
    public void RunBeforeAnyTests()
    {
        _dbContextMock = new Mock<IApplicationDbContext>();
    }

    [OneTimeTearDown]
    public void RunAfterAnyTests()
    {
        // _applicationDbContextMock.Database.EnsureDeleted();
    }
}
