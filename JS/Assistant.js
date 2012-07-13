var menuHTML="<ul id='navmenu'><li>"+
"<a href='#'>Master</a><ul><li>"+
"<a href='#'>Access Rights-></a><ul>"+
"<li><a href='/WMS/View/Forms/Master/UserDefault.aspx'>User</a></li>"+
"<li><a href='/WMS/View/Forms/Master/RoleDefault.aspx'>Role</a></li>"+
"<li><a href='/WMS/View/Forms/Master/Privilege.aspx'>Privilege</a></li>"+
"<li><a href='/WMS/View/Forms/Master/ChangPassword.aspx'>ChangPassword</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>Culprit-></a><ul>"+
"<li><a href='/WMS/View/Forms/Master/CulpritDefault.aspx'>Culprit</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>CustumerVoice-></a><ul>"+
"<li><a href='/WMS/View/Forms/Master/CustumerVoiceDefault.aspx'>CustumerVoice</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>Defect-></a><ul>"+
"<li><a href='/WMS/View/Forms/Master/DefectDefault.aspx'>Defect</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>Item-></a><ul>"+
"<li><a href='/WMS/View/Forms/Master/ItemDefault.aspx'>Item</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>Model-></a><ul>"+
"<li><a href='/WMS/View/Forms/Master/ModelDefault.aspx'>Model</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>Dealer-></a><ul>"+
"<li><a href='/WMS/View/Forms/Master/DealerDefault.aspx'>Dealer</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>GroupSetting-></a><ul>"+
"<li><a href='/WMS/View/Forms/Setting/CulpritGroupSetting.aspx'>CulpritGroupSetting</a></li>"+
"<li><a href='/WMS/View/Forms/Setting/CustomerVoiceGroupSetting.aspx'>CustomerVoiceGroupSetting</a></li>"+
"<li><a href='/WMS/View/Forms/Setting/DefectGroupSetting.aspx'>DefectGroupSetting</a></li>"+
"<li><a href='/WMS/View/Forms/Setting/ItemGroupSettingt.aspx'>ItemGroupSetting</a></li>"+
"<li><a href='/WMS/View/Forms/Setting/ModelGroupSetting.aspx'>ModelGroupSetting</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>BaseProductionMonth-></a><ul>"+
"<li><a href='/WMS/View/Forms/Master/BaseProductionMonth.aspx'>BaseProductionMonth</a></li>"+
"</ul>"+
"</li><li>"+
"</li></ul></li><li>"+
"<a href='#'>Configurator</a><ul><li>"+
"<a href='#'>CulpritConfigurator-></a><ul>"+
"<li><a href='/WMS/View/Forms/Configurator/CulpritConfigurator.aspx'>CulpritConfigurator</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>CVoiceConfigurator-></a><ul>"+
"<li><a href='/WMS/View/Forms/Configurator/CVoiceConfigurator.aspx'>CVoiceConfigurator</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>DefectConfigurator-></a><ul>"+
"<li><a href='/WMS/View/Forms/Configurator/DefectConfigurator.aspx'>DefectConfigurator</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>ItemConfigurator-></a><ul>"+
"<li><a href='/WMS/View/Forms/Configurator/ItemConfigurator.aspx'>ItemConfigurator</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>LabelConfigurator-></a><ul>"+
"<li><a href='/WMS/View/Forms/Configurator/LabelConfig.aspx'>LabelConfigurator</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>ModelConfigurato-></a><ul>"+
"<li><a href='/WMS/View/Forms/Configurator/Model.aspx'>ModelConfigurato</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>ProductGroupMapping-></a><ul>"+
"<li><a href='/WMS/View/Forms/Configurator/ProductGroupMapping.aspx'>ProductGroupMapping</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>MonthOpenClose-></a><ul>"+
"<li><a href='/WMS/View/Forms/Configurator/MonthOpenClose.aspx'>MonthOpenClose</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>Setting-></a><ul>"+
"<li><a href='/WMS/View/Forms/Configurator/ShowCulpritCodeByGroup.aspx'>ShowCulpritCodeByGroup</a></li>"+
"<li><a href='/WMS/View/Forms/Configurator/ShowCVoiceCodeByGroup.aspx'>ShowCVoiceCodeByGroup</a></li>"+
"<li><a href='/WMS/View/Forms/Configurator/ShowDefectCodeByGroup.aspx'>ShowDefectCodeByGroup</a></li>"+
"<li><a href='/WMS/View/Forms/Configurator/ShowItemCodeByGroupName.aspx'>ShowItemCodeByGroupName</a></li>"+
"<li><a href='/WMS/View/Forms/Configurator/ShowProductCodebyGroup.aspx'>ShowProductCodebyGroup</a></li>"+
"</ul>"+
"</li><li>"+
"</li></ul></li><li>"+
"<a href='#'>Import</a><ul><li>"+
"<a href='#'>Import-></a><ul>"+
"<li><a href='/WMS/View/Forms/Master/FileImport.aspx'>Import</a></li>"+
"</ul>"+
"</li><li>"+
"</li></ul></li><li>"+
"<a href='#'>Report</a><ul><li>"+
"<a href='#'>CvoiceGRoupWiseACR-></a><ul>"+
"<li><a href='/WMS/View/Forms/Reports/CvoiceGRoupWiseACR.aspx'>CvoiceGRoupWiseACR</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>DateWiseCostingReport-></a><ul>"+
"<li><a href='/WMS/View/Forms/Reports/DateWiseCostingReport .aspx'>DateWiseCostingReport</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>DealerWiseACR-></a><ul>"+
"<li><a href='/WMS/View/Forms/Reports/DealerWiseACR.aspx'>DealerWiseACR</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>DefectGroupWiseACR-></a><ul>"+
"<li><a href='/WMS/View/Forms/Reports/DefectGroupWiseACR.aspx'>DefectGroupWiseACR</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>ItemGRoupWiseACRDetail-></a><ul>"+
"<li><a href='/WMS/View/Forms/Reports/ItemGRoupWiseACRDetail.aspx'>ItemGRoupWiseACRDetail</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>ModelWiseACRDetail-></a><ul>"+
"<li><a href='/WMS/View/Forms/Reports/ModelWiseACRDetail.aspx'>ModelWiseACRDetail</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>NotAssignGroupDetail-></a><ul>"+
"<li><a href='/WMS/View/Forms/Reports/NotAssignGroupDetail.aspx'>NotAssignGroupDetail</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>ACRDynamicReport-></a><ul>"+
"<li><a href='/WMS/View/Forms/Reports/ACRDynamicReport.aspx'>ACRDynamicReport</a></li>"+
"</ul>"+
"</li><li>"+
"<a href='#'>CulpritWiseACRDetail-></a><ul>"+
"<li><a href='/WMS/View/Forms/Reports/CulpritWiseACRDetail.aspx'>CulpritWiseACRDetail</a></li>"+
"</ul>"+
"</li><li>"+
"</li></ul></li><li>"+
"</li></ul>";
document.write(menuHTML);
