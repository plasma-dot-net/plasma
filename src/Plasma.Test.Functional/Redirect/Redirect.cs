/* **********************************************************************************
 *
 * Copyright (c) Microsoft Corporation. All rights reserved.
 *
 * This source code is subject to terms and conditions of the Microsoft Permissive
 * License (MS-PL).
 *
 * You must not remove this notice, or any other, from this software.
 *
 * **********************************************************************************/
using NUnit.Framework;
using OpenQA.Selenium;
using Plasma.WebDriver;

namespace Plasma.Test.Functional.Redirect
{
    [TestFixture]
    public class Redirect
    {
        [Test]
        public void Basic_Redirect()
        {
            /////////////////////////////////////////////////////////////////////////////
            // Test Verifying a Redirect on Redirect.aspx
            var driver = new PlasmaDriver(WebApplicationFixture.AppInstance);
            driver.Navigate().GoToUrl("~/Basic/Redirect.aspx");
            
            var titleElement = driver.FindElement(By.TagName("title"));

            Assert.AreEqual("Query String", titleElement.Text);
        }
    }
}
