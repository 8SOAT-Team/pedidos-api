﻿// ------------------------------------------------------------------------------
//  <auto-generated>
//      This code was generated by SpecFlow (https://www.specflow.org/).
//      SpecFlow Version:3.9.0.0
//      SpecFlow Generator Version:3.9.0.0
// 
//      Changes to this file may cause incorrect behavior and will be lost if
//      the code is regenerated.
//  </auto-generated>
// ------------------------------------------------------------------------------
#region Designer generated code
#pragma warning disable
namespace Pedidos.Tests.BDD.Feature
{
    using TechTalk.SpecFlow;
    using System;
    using System.Linq;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public partial class GerenciamentoDeClientesFeature : object, Xunit.IClassFixture<GerenciamentoDeClientesFeature.FixtureData>, System.IDisposable
    {
        
        private static TechTalk.SpecFlow.ITestRunner testRunner;
        
        private static string[] featureTags = ((string[])(null));
        
        private Xunit.Abstractions.ITestOutputHelper _testOutputHelper;
        
#line 1 "Cliente.feature"
#line hidden
        
        public GerenciamentoDeClientesFeature(GerenciamentoDeClientesFeature.FixtureData fixtureData, Pedidos_Tests_XUnitAssemblyFixture assemblyFixture, Xunit.Abstractions.ITestOutputHelper testOutputHelper)
        {
            this._testOutputHelper = testOutputHelper;
            this.TestInitialize();
        }
        
        public static void FeatureSetup()
        {
            testRunner = TechTalk.SpecFlow.TestRunnerManager.GetTestRunner();
            TechTalk.SpecFlow.FeatureInfo featureInfo = new TechTalk.SpecFlow.FeatureInfo(new System.Globalization.CultureInfo("pt-BR"), "BDD/Feature", "Gerenciamento de Clientes", "  Como um sistema de gerenciamento de clientes\r\n  Eu quero garantir que os client" +
                    "es sejam criados, alterados e validados corretamente", ProgrammingLanguage.CSharp, featureTags);
            testRunner.OnFeatureStart(featureInfo);
        }
        
        public static void FeatureTearDown()
        {
            testRunner.OnFeatureEnd();
            testRunner = null;
        }
        
        public void TestInitialize()
        {
        }
        
        public void TestTearDown()
        {
            testRunner.OnScenarioEnd();
        }
        
        public void ScenarioInitialize(TechTalk.SpecFlow.ScenarioInfo scenarioInfo)
        {
            testRunner.OnScenarioInitialize(scenarioInfo);
            testRunner.ScenarioContext.ScenarioContainer.RegisterInstanceAs<Xunit.Abstractions.ITestOutputHelper>(_testOutputHelper);
        }
        
        public void ScenarioStart()
        {
            testRunner.OnScenarioStart();
        }
        
        public void ScenarioCleanup()
        {
            testRunner.CollectScenarioErrors();
        }
        
        void System.IDisposable.Dispose()
        {
            this.TestTearDown();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Criar um cliente válido")]
        [Xunit.TraitAttribute("FeatureTitle", "Gerenciamento de Clientes")]
        [Xunit.TraitAttribute("Description", "Criar um cliente válido")]
        public void CriarUmClienteValido()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Criar um cliente válido", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 7
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 8
    testRunner.Given("que o CPF do cliente é \"765.273.200-00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 9
    testRunner.And("o nome do cliente é \"João da Silva\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 10
    testRunner.And("o email do cliente é \"joao@example.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 11
    testRunner.When("eu criar o cliente", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 12
    testRunner.Then("o cliente deve ser válido", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Retornar exceção quando CPF for nulo ou vazio")]
        [Xunit.TraitAttribute("FeatureTitle", "Gerenciamento de Clientes")]
        [Xunit.TraitAttribute("Description", "Retornar exceção quando CPF for nulo ou vazio")]
        public void RetornarExcecaoQuandoCPFForNuloOuVazio()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Retornar exceção quando CPF for nulo ou vazio", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 14
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 15
    testRunner.Given("que o CPF do cliente é \"\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 16
    testRunner.And("o nome do cliente é \"João da Silva\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 17
    testRunner.And("o email do cliente é \"joao@example.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 18
    testRunner.When("eu tentar criar o cliente", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 19
    testRunner.Then("uma exceção de validação deve ser lançada", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Retornar exceção quando CPF for inválido")]
        [Xunit.TraitAttribute("FeatureTitle", "Gerenciamento de Clientes")]
        [Xunit.TraitAttribute("Description", "Retornar exceção quando CPF for inválido")]
        public void RetornarExcecaoQuandoCPFForInvalido()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Retornar exceção quando CPF for inválido", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 21
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 22
    testRunner.Given("que o CPF do cliente é \"765.273.200-0X\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 23
    testRunner.And("o nome do cliente é \"Nome válido\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 24
    testRunner.And("o email do cliente é \"email@example.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 25
    testRunner.When("eu tentar criar o cliente", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 26
    testRunner.Then("uma exceção de validação deve ser lançada", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Retornar exceção quando nome for inválido")]
        [Xunit.TraitAttribute("FeatureTitle", "Gerenciamento de Clientes")]
        [Xunit.TraitAttribute("Description", "Retornar exceção quando nome for inválido")]
        public void RetornarExcecaoQuandoNomeForInvalido()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Retornar exceção quando nome for inválido", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 28
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 29
    testRunner.Given("que o CPF do cliente é \"765.273.200-00\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 30
    testRunner.And("o nome do cliente é \"jo\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 31
    testRunner.And("o email do cliente é \"email@example.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 32
    testRunner.When("eu tentar criar o cliente", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 33
    testRunner.Then("uma exceção de validação deve ser lançada", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Alterar nome de um cliente")]
        [Xunit.TraitAttribute("FeatureTitle", "Gerenciamento de Clientes")]
        [Xunit.TraitAttribute("Description", "Alterar nome de um cliente")]
        public void AlterarNomeDeUmCliente()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Alterar nome de um cliente", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 35
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 36
    testRunner.Given("que o cliente foi criado com CPF \"765.273.200-00\", nome \"João da Silva\" e email \"" +
                        "joao@example.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 37
    testRunner.And("o novo nome do cliente é \"João Silva\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 38
    testRunner.When("eu alterar o nome do cliente", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 39
    testRunner.Then("o nome do cliente deve ser \"João Silva\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [Xunit.SkippableFactAttribute(DisplayName="Alterar email de um cliente")]
        [Xunit.TraitAttribute("FeatureTitle", "Gerenciamento de Clientes")]
        [Xunit.TraitAttribute("Description", "Alterar email de um cliente")]
        public void AlterarEmailDeUmCliente()
        {
            string[] tagsOfScenario = ((string[])(null));
            System.Collections.Specialized.OrderedDictionary argumentsOfScenario = new System.Collections.Specialized.OrderedDictionary();
            TechTalk.SpecFlow.ScenarioInfo scenarioInfo = new TechTalk.SpecFlow.ScenarioInfo("Alterar email de um cliente", null, tagsOfScenario, argumentsOfScenario, featureTags);
#line 41
  this.ScenarioInitialize(scenarioInfo);
#line hidden
            if ((TagHelper.ContainsIgnoreTag(tagsOfScenario) || TagHelper.ContainsIgnoreTag(featureTags)))
            {
                testRunner.SkipScenario();
            }
            else
            {
                this.ScenarioStart();
#line 42
    testRunner.Given("que o cliente foi criado com CPF \"765.273.200-00\", nome \"João da Silva\" e email \"" +
                        "joao@example.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Dado ");
#line hidden
#line 43
    testRunner.And("o novo email do cliente é \"joao.da.silva@example.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "E ");
#line hidden
#line 44
    testRunner.When("eu alterar o email do cliente para \"joao.da.silva@example.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Quando ");
#line hidden
#line 45
    testRunner.Then("o email do cliente deve ser \"joao.da.silva@example.com\"", ((string)(null)), ((TechTalk.SpecFlow.Table)(null)), "Então ");
#line hidden
            }
            this.ScenarioCleanup();
        }
        
        [System.CodeDom.Compiler.GeneratedCodeAttribute("TechTalk.SpecFlow", "3.9.0.0")]
        [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
        public class FixtureData : System.IDisposable
        {
            
            public FixtureData()
            {
                GerenciamentoDeClientesFeature.FeatureSetup();
            }
            
            void System.IDisposable.Dispose()
            {
                GerenciamentoDeClientesFeature.FeatureTearDown();
            }
        }
    }
}
#pragma warning restore
#endregion
