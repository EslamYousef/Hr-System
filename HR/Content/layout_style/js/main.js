/*global $*/
/*global console*/
$(function () {
    'use strict';
    
//    $('.main-tab').click(function () {
//        $('.' + $(this).data('value')).siblings('.row').hide();
//        if ($('.' + $(this).data('value')).is(':hidden')) {
//            
//            $('.' + $(this).data('value')).css('display', 'flex');
//        } else {
//            $('.' + $(this).data('value')).hide();
//        }
//    });
    
    
});
function allBasic() {
    'use strict';
    $(".child-tabs .row:not(.basic)").empty();
    var basic = "<div class='col-md-4'> <div class='tab' id='setup' data-value='setup' onclick='allSetup()'> <p><i class='fab fa-500px'></i> Setup</p></div></div><div class='col-md-4'> <div class='tab' id='cards' onclick='allCards()'> <p><i class='fab fa-500px'></i> Cards</p></div></div><div class='col-md-4'> <div class='tab' id='transaction'onclick='allTransaction()'> <p><i class='fab fa-500px'></i> Transaction</p></div></div><div class='col-md-4'> <div class='tab' id='process' onclick='allProcess()'> <p><i class='fab fa-500px'></i> Process</p></div></div><div class='col-md-4'> <div class='tab' id='inquery' onclick='allInquery()'> <p><i class='fab fa-500px'></i> Inquery</p></div></div><div class='col-md-4'> <div class='tab' id='report' onclick='allReport()'> <p><i class='fab fa-500px'></i> Report</p></div></div>",
        content = document.querySelector('.basic'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.basic').empty();
    } else {
        $('.basic').append(basic);
    }

}

function allSetup() {
    'use strict';
    $(".child-tabs .row:not(.basic,.setup)").empty();
    var setup = "<div class='col-md-4'> <div class='tab' id='address' onclick='address_bas()'> <p><i class='fab fa-500px'></i> Address</p></div></div><div class='col-md-4'> <div class='tab' id='nationality'> <p><i class='fab fa-500px'></i> <a href='/Nationality'>Nationality</a></p></div></div><div class='col-md-4'> <div class='tab' id='religion'> <p><i class='fab fa-500px'></i> <a href='/Religion'>Religion</a></p></div></div><div class='col-md-4'> <div class='tab' id='qualifications' onclick='qualifications()'> <p><i class='fab fa-500px'></i> Qualifications</p></div></div><div class='col-md-4'> <div class='tab' id='milirty' onclick='milirty()'> <p><i class='fab fa-500px'></i> Milirty</p></div></div><div class='col-md-4'> <div class='tab' id='documents' onclick='documents()'> <p><i class='fab fa-500px'></i> Documents</p></div></div><div class='col-md-4' id='currency' onclick='currency()'> <div class='tab'> <p><i class='fab fa-500px'></i> Currency</p></div></div><div class='col-md-4'> <div class='tab' id='check_request' onclick='check_request()'> <p><i class='fab fa-500px'></i> Check Request</p></div></div><div class='col-md-4'> <div class='tab' id='contact_method'> <p><i class='fab fa-500px'></i> <a href='/Contact_methods'>Contact Method</a></p></div></div><div class='col-md-4'> <div class='tab' id='rejection_resons'> <p><i class='fab fa-500px'></i> <a href='/Rejection_Reasons'>Rejection Reasons</p></div></div><div class='col-md-4'> <div class='tab' id='external_companies'> <p><i class='fab fa-500px'></i> <a href='/External_companies'>External Companies</a></p></div></div><div class='col-md-4'> <div class='tab' id='air_ports'> <p><i class='fab fa-500px'></i> <a href='/Air_ports'>Air Ports</a></p></div></div><div class='col-md-4'> <div class='tab' id='tickets_prices'> <p><i class='fab fa-500px'></i> <a href='/Ticket_Prices'>Tickets Prices</a></p></div></div><div class='col-md-4'> <div class='tab' id='sponsor'> <p><i class='fab fa-500px'></i> <a href='/Sponsor'>Sponsor</a></p></div></div>",
        content = document.querySelector('.setup'),
        result = content.innerHTML.trim();
    if (result !== "") {
        $('.setup').empty();
    } else {
        $('.setup').append(setup);
    }

}

function allCards() {
    'use strict';
    $(".child-tabs .row:not(.basic,.cards)").empty();
    var cards = "",
        content = document.querySelector('.cards'),
        result = content.innerHTML.trim();
    if (result !== "") {
        $('.cards').empty();
    } else {
        $('.cards').append(cards);
    }

}

function allTransaction() {
    'use strict';
    $(".child-tabs .row:not(.basic,.transaction)").empty();
    var transaction = "<div class='col-md-4'> <div class='tab'> <p><i class='fab fa-500px'></i><a href='/Check_Request/Create'> Check request</a></p></div></div>",
        content = document.querySelector('.transaction'),
        result = content.innerHTML.trim();
    if (result !== "") {
        $('.transaction').empty();
    } else {
        $('.transaction').append(transaction);
    }

}

function allProcess() {
    'use strict';
    $(".child-tabs .row:not(.basic,.process)").empty();
    var process = "<div class='col-md-4'> <div class='tab'> <p><i class='fab fa-500px'></i><a href='/Check_Request'> Check request action</a></p></div></div>",
        content = document.querySelector('.process'),
        result = content.innerHTML.trim();
    if (result !== "") {
        $('.process').empty();
    } else {
        $('.process').append(process);
    }
}

function allInquery() {
    'use strict';
    $(".child-tabs .row:not(.basic,.inquery)").empty();
    var inquery = "",
        content = document.querySelector('.inquery'),
        result = content.innerHTML.trim();
    if (result !== "") {
        $('.inquery').empty();
    } else {
        $('.inquery').append(inquery);
    }

}

function allReport() {
    'use strict';
    $(".child-tabs .row:not(.basic,.report)").empty();
    var report = "",
        content = document.querySelector('.report'),
        result = content.innerHTML.trim();
    if (result !== "") {
        $('.report').empty();
    } else {
        $('.report').append(report);
    }

}
///////////////////////////////////////////////////////////////////////////////
//ALL Basic Setup Address 
function address_bas() {
    'use strict';
    $(".child-tabs .row:not(.basic,.setup,.address)").empty();
    var address = "<div class='col-md-4'> <div class='tab' id='country'> <p><i class='fab fa-500px'></i> <a href='/Country'>Country</a></p></div></div><div class='col-md-4'> <div class='tab' id='regions'> <p><i class='fab fa-500px'></i> <a href='/Area'>Regions</a></p></div></div><div class='col-md-4'> <div class='tab' id='state'> <p><i class='fab fa-500px'></i> <a href='/State'>State/government</a></p></div></div><div class='col-md-4'> <div class='tab' id='county'> <p><i class='fab fa-500px'></i> <a href='/territory'>County</a></p></div></div><div class='col-md-4'> <div class='tab' id='City'> <p><i class='fab fa-500px'></i> <a href='/Cites'>City</a></p></div></div><div class='col-md-4'> <div class='tab' id='zip'> <p><i class='fab fa-500px'></i> <a href='/Postalcode'>Zip postal</a></p></div></div>",
        content = document.querySelector('.address'),
        result = content.innerHTML.trim();
    if (result !== "") {
        $('.address').empty();
    } else {
        $('.address').append(address);
    }

}

function qualifications() {
    'use strict';
    $(".child-tabs .row:not(.basic,.setup,.qualifications)").empty();
    var qualification = "<div class='col-md-4'> <div class='tab' id='qualification_category'> <p><i class='fab fa-500px'></i> <a href='/Educate_category'>Qualification category</a></p></div></div><div class='col-md-4'> <div class='tab' id='educate_title'> <p><i class='fab fa-500px'></i> <a href='/Educate_title'>Education title</a></p></div></div><div class='col-md-4'> <div class='tab' id='qulification_main_provider'> <p><i class='fab fa-500px'></i> <a href='/EducateMainBody'>Qulification main provider</a></p></div></div><div class='col-md-4'> <div class='tab' id='qulification_sub_providor'> <p><i class='fab fa-500px'></i> <a href='/Sub_educational_body'>Qulification sub providor</a></p></div></div><div class='col-md-4'> <div class='tab' id='qulification_name'> <p><i class='fab fa-500px'></i> <a href='/Name_of_educational_qualification'>Qulification name</a></p></div></div><div class='col-md-4'> <div class='tab' id='qulification_degree'> <p><i class='fab fa-500px'></i> <a href='/GradeEducate'>Qulification degree</a></p></div></div><div class='col-md-4'> <div class='tab' id='qulification_specialty'> <p><i class='fab fa-500px'></i> <a href='/QualificationMajor'>Qulification specialty</a></p></div></div>",
        content = document.querySelector('.qualifications'),
        result = content.innerHTML.trim();
    if (result !== "") {
        $('.qualifications').empty();
    } else {
        $('.qualifications').append(qualification);
    }

}

function milirty() {
    'use strict';
    $(".child-tabs .row:not(.basic,.setup,.milirty)").empty();
    var milirtys = "<div class='col-md-4'> <div class='tab' id='miliraty_service_status'> <p><i class='fab fa-500px'></i> <a href='/Military_Service_Status'>Miliraty service status</a></p></div></div><div class='col-md-4'> <div class='tab' id='miliraty_service_rank'> <p><i class='fab fa-500px'></i> <a href='/Military_Service_Rank'>Miliraty service rank</a></p></div></div>",
        content = document.querySelector('.milirty'),
        result = content.innerHTML.trim();
    if (result !== "") {
        $('.milirty').empty();
    } else {
        $('.milirty').append(milirtys);
    }

}

function documents() {
    'use strict';
    $(".child-tabs .row:not(.basic,.setup,.documents)").empty();
    var documentss = "<div class='col-md-4'> <div class='tab' id='document_group'> <p><i class='fab fa-500px'></i> <a href='/Document_Group'>Documents Group</a></p></div></div><div class='col-md-4'> <div class='tab' id='Document'> <p><i class='fab fa-500px'></i> <a href='/Documents'>Document</a></p></div></div>",
        content = document.querySelector('.documents'),
        result = content.innerHTML.trim();
    if (result !== "") {
        $('.documents').empty();
    } else {
        $('.documents').append(documentss);
    }
}

function currency() {
    'use strict';
    $(".child-tabs .row:not(.basic,.setup,.currency)").empty();
    var currencys = "<div class='col-md-4'> <div class='tab' id='currency'> <p><i class='fab fa-500px'></i> <a href='/Currency'>Currency</a></p></div></div><div class='col-md-4'> <div class='tab' id='exchange_rate'> <p><i class='fab fa-500px'></i> <a href='/ExchangeRate'>Exchange Rate</a></p></div></div>",
        content = document.querySelector('.currency'),
        result = content.innerHTML.trim();
    if (result !== "") {
        $('.currency').empty();
    } else {
        $('.currency').append(currencys);
    }
}

function check_request() {
    'use strict';
    $(".child-tabs .row:not(.basic,.setup,.check_request)").empty();
    var check_requests = "<div class='col-md-4'> <div class='tab' id='check_type'> <p><i class='fab fa-500px'></i> <a href='/Checktype'>Check type</a></p></div></div><div class='col-md-4'> <div class='tab' id='check_status'> <p><i class='fab fa-500px'></i> <a href='/checkstatus'>Check status</a></p></div></div>",
        content = document.querySelector('.check_request'),
        result = content.innerHTML.trim();
    if (result !== "") {
        $('.check_request').empty();
    } else {
        $('.check_request').append(check_requests);
    }
}

//ORGANIZATION
function allOrg() {
    'use strict';
    $(".child-tabs .row:not(.organization)").empty();
    var org = "<div class='col-md-4'> <div class='tab' id='setup_Org' data-value='setup' onclick='allSetupOrg()'> <p><i class='fab fa-500px'></i> Setup</p></div></div><div class='col-md-4'> <div class='tab' id='cards_Org' onclick='allCardsOrg()'> <p><i class='fab fa-500px'></i> Cards</p></div></div><div class='col-md-4'> <div class='tab' id='transaction_Org' onclick='allTransactionOrg()'> <p><i class='fab fa-500px'></i> Transaction</p></div></div><div class='col-md-4'> <div class='tab' id='process_Org' onclick='allProcessOrg()'> <p><i class='fab fa-500px'></i> Process</p></div></div><div class='col-md-4'> <div class='tab' id='inquery_Org' onclick='allInqueryOrg()'> <p><i class='fab fa-500px'></i> Inquery</p></div></div><div class='col-md-4'> <div class='tab' id='report_Org' onclick='allReportOrg()'> <p><i class='fab fa-500px'></i> Report</p></div></div>",
        content = document.querySelector('.organization'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.organization').empty();
    } else {
        $('.organization').append(org);
    }

}

function allSetupOrg() {
    'use strict';
    $(".child-tabs .row:not(.organization,.setup_org)").empty();
    var setup_org = "<div class='col-md-4'> <div class='tab' id='authority' data-value='authority' onclick='authorityOrg()'> <p><i class='fab fa-500px'></i> Authority</p></div></div><div class='col-md-4'> <div class='tab' id='skills_Org' onclick='skillsOrg()'> <p><i class='fab fa-500px'></i> Skills</p></div></div><div class='col-md-4'> <div class='tab' id='risk_Org' onclick='riskOrg()'> <p><i class='fab fa-500px'></i> Risk</p></div></div><div class='col-md-4'> <div class='tab' id='requirments_Org''> <p><i class='fab fa-500px'></i> <a href='/Requirments'>Requirments</a></p></div></div><div class='col-md-4'> <div class='tab' id='Experience_Org'> <p><i class='fab fa-500px'></i> <a href='/Experience_group'>Experience group</a></p></div></div><div class='col-md-4'> <div class='tab' id='responsibilites_Org'> <p><i class='fab fa-500px'></i> <a href='Responsibilites'>Rsponsibilites</a></p></div></div><div class='col-md-4'> <div class='tab' id='worklocation_Org'> <p><i class='fab fa-500px'></i> <a href='work_location'>work location</a></p></div></div><div class='col-md-4'> <div class='tab' id='jobLevel_Org' onclick='job_level()'> <p><i class='fab fa-500px'></i> Job Level</p></div></div><div class='col-md-4'> <div class='tab' id='job_title_Org' onclick='job_title()'> <p><i class='fab fa-500px'></i> Job Title</p></div></div><div class='col-md-4'> <div class='tab' id='job_title_Org' onclick='org_chart()'> <p><i class='fab fa-500px'></i> Organization chart</p></div></div><div class='col-md-4'> <div class='tab' id='budget_Org' onclick='budget()'> <p><i class='fab fa-500px'></i>Budget</p></div></div>",
        content = document.querySelector('.setup_org'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.setup_org').empty();
    } else {
        $('.setup_org').append(setup_org);
    }
}

function allCardsOrg() {
    'use strict';
    $(".child-tabs .row:not(.organization,.cards_org)").empty();
    var cards_org = "<div class='col-md-4'> <div class='tab' id='job_level_Org'> <p><i class='fab fa-500px'></i> <a href='/job_level_setup'>Job level</a></p></div></div><div class='col-md-4'> <div class='tab' id='job_title_card_Org'> <p><i class='fab fa-500px'></i> <a href='/job_title_card'>Job title card</a></p></div></div><div class='col-md-4'> <div class='tab' id='org_chart_card_Org'> <p><i class='fab fa-500px'></i> <a href='/OrganizationChart'>Organization chart card</a></p></div></div><div class='col-md-4'> <div class='tab' id='budget_Card'> <p><i class='fab fa-500px'></i> <a href='/Budget'>Budget Card</a></p></div></div><div class='col-md-4'> <div class='tab' id='inquery_Org'> <p><i class='fab fa-500px'></i> <a href='/manpower'>Mainpower</a></p></div></div>",
        content = document.querySelector('.cards_org'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.cards_org').empty();
    } else {
        $('.cards_org').append(cards_org);
    }
}

function allTransactionOrg() {
    'use strict';
    $(".child-tabs .row:not(.organization,.transaction_org)").empty();
    var transaction_org = "",
        content = document.querySelector('.cards_org'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.transaction_org').empty();
    } else {
        $('.transaction_org').append(transaction_org);
    }
}

function allProcessOrg() {
    'use strict';
    $(".child-tabs .row:not(.organization,.process_org)").empty();
    var process_org = "",
        content = document.querySelector('.process_org'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.process_org').empty();
    } else {
        $('.process_org').append(process_org);
    }
}

function allInqueryOrg() {
    'use strict';
    $(".child-tabs .row:not(.organization,.inquery_org)").empty();
    var inquery_org = "<div class='col-md-4'> <div class='tab' id='view_jobs_Org'> <p><i class='fab fa-500px'></i> <a href='/View_jobs'>View Jobs</a></p></div></div><div class='col-md-4'> <div class='tab' id='org_view_Org'> <p><i class='fab fa-500px'></i> <a href='/organization_view_related_to_jobs'>Organization view related to jobs</a></p></div></div>",
        content = document.querySelector('.inquery_org'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.inquery_org').empty();
    } else {
        $('.inquery_org').append(inquery_org);
    }
}

function allReportOrg() {
    'use strict';
    $(".child-tabs .row:not(.organization,.report_org)").empty();
    var report_org = "<div class='col-md-4'> <div class='tab' id='job_title_card_Org'> <p><i class='fab fa-500px'></i> <a href='/job_title'>Job title card</a></p></div></div><div class='col-md-4'> <div class='tab' id='org_chart_Org'> <p><i class='fab fa-500px'></i> <a href='/Chart'>Organization Chart</a></p></div></div>",
        content = document.querySelector('.report_org'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.report_org').empty();
    } else {
        $('.report_org').append(report_org);
    }
}

function authorityOrg() {
    'use strict';
    $(".child-tabs .row:not(.organization,.setup_org,.authority)").empty();
    var authority = "<div class='col-md-4'> <div class='tab' id='Authority_Org'> <p><i class='fab fa-500px'></i> <a href='/Authority_Type'>Authority type</a></p></div></div><div class='col-md-4'> <div class='tab' id='org_chart_Org'> <p><i class='fab fa-500px'></i> <a href='/Authorities'>Authorities</a></p></div></div>",
        content = document.querySelector('.authority'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.authority').empty();
    } else {
        $('.authority').append(authority);
    }
}

function skillsOrg() {
    'use strict';
    $(".child-tabs .row:not(.organization,.setup_org,.skills)").empty();
    var skills = "<div class='col-md-4'> <div class='tab' id='Skill_group_Org'> <p><i class='fab fa-500px'></i> <a href='/Skill_group'>Skills Group</a></p></div></div><div class='col-md-4'> <div class='tab' id='Skill_Org'> <p><i class='fab fa-500px'></i> <a href='/Skill'>Skill</a></p></div></div>",
        content = document.querySelector('.skills'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.skills').empty();
    } else {
        $('.skills').append(skills);
    }
}

function riskOrg() {
    'use strict';
    $(".child-tabs .row:not(.organization,.setup_org,.risk)").empty();
    var risk = "<div class='col-md-4'> <div class='tab' id='Risks_type_Org'> <p><i class='fab fa-500px'></i> <a href='/Risks_type'>Risk Type</a></p></div></div><div class='col-md-4'> <div class='tab' id='Skill_Org'> <p><i class='fab fa-500px'></i> <a href='/Risk'>Risks</a></p></div></div>",
        content = document.querySelector('.risk'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.risk').empty();
    } else {
        $('.risk').append(risk);
    }
}

function job_level() {
    'use strict';
    $(".child-tabs .row:not(.organization,.setup_org,.joblevel)").empty();
    var joblevel = "<div class='col-md-4'> <div class='tab' id='Job_level_class_Org'> <p><i class='fab fa-500px'></i> <a href='/Job_level_class'>Job level class</a></p></div></div><div class='col-md-4'> <div class='tab' id='Job_level_grade_Org'> <p><i class='fab fa-500px'></i> <a href='/Job_level_grade'>Job level grade</a></p></div></div>",
        content = document.querySelector('.joblevel'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.joblevel').empty();
    } else {
        $('.joblevel').append(joblevel);
    }
}

function job_title() {
    'use strict';
    $(".child-tabs .row:not(.organization,.setup_org,.jobTitle)").empty();
    var jobTitle = "<div class='col-md-4'> <div class='tab' id='Job_title_class_Org'> <p><i class='fab fa-500px'></i> <a href='/job_title_class'>Job title class</a></p></div></div><div class='col-md-4'> <div class='tab' id='Job_title_sup_class_Org'> <p><i class='fab fa-500px'></i> <a href='/Job_title_Sup_class'>Job title sub class</a></p></div></div>",
        content = document.querySelector('.jobTitle'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.jobTitle').empty();
    } else {
        $('.jobTitle').append(jobTitle);
    }
}

function org_chart() {
    'use strict';
    $(".child-tabs .row:not(.organization,.setup_org,.orgChart)").empty();
    var orgChart = "<div class='col-md-4'><div class='tab' id='Organization_unit_schema_Org'><p><i class='fab fa-500px'></i> <a href='/Organization_unit_schema'>Organization unit schema</a></p></div></div><div class='col-md-4'><div class='tab' id='OrganizationLevelUnit_Org'><p><i class='fab fa-500px'></i> <a href='/OrganizationLevelUnit'>Organization unit level</a></p></div></div><div class='col-md-4'><div class='tab' id='Organizationunittype_Org'><p><i class='fab fa-500px'></i> <a href='/Organizationunittype'>Organization unit type</a></p></div></div>",
        content = document.querySelector('.orgChart'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.orgChart').empty();
    } else {
        $('.orgChart').append(orgChart);
    }
}

function budget() {
    'use strict';
    $(".child-tabs .row:not(.organization,.setup_org,.budgetOrg)").empty();
    var budgetOrg = "<div class='col-md-4'><div class='tab' id='Budget_class_Org'><p><i class='fab fa-500px'></i> <a href='/Budget_class'>Budget class</a></p></div></div><div class='col-md-4'><div class='tab' id='Budget_class_items'><p><i class='fab fa-500px'></i> <a href='/Budget_class_items'>Budget class items</a></p></div></div>",
        content = document.querySelector('.budgetOrg'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.budgetOrg').empty();
    } else {
        $('.budgetOrg').append(budgetOrg);
    }
}


//Personnel
function allPersonnel() {
    'use strict';
    $(".child-tabs .row:not(.personnel)").empty();
    var personnel = "<div class='col-md-4'><div class='tab' id='setup_personnel' data-value='setup' onclick='allSetupPersonnel()'><p><i class='fab fa-500px'></i> Setup</p></div></div><div class='col-md-4'><div class='tab' id='cards_personnel' onclick='allCardsPersonnel()'><p><i class='fab fa-500px'></i> Cards</p></div></div><div class='col-md-4'><div class='tab' id='transaction_personnel' onclick='allTransactionPersonnel()'><p><i class='fab fa-500px'></i> Transaction</p></div></div><div class='col-md-4'><div class='tab' id='process_personnel' onclick='allProcessPersonnel()'><p><i class='fab fa-500px'></i> Process</p></div></div><div class='col-md-4'><div class='tab' id='inquery_personnel' onclick='allInqueryPersonnel()'><p><i class='fab fa-500px'></i> Inquery</p></div></div><div class='col-md-4'><div class='tab' id='report_personnel' onclick='allReportPersonnel()'><p><i class='fab fa-500px'></i> Report</p></div></div>",
        content = document.querySelector('.personnel'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.personnel').empty();
    } else {
        $('.personnel').append(personnel);
    }
}

function allSetupPersonnel() {
    'use strict';
    $(".child-tabs .row:not(.personnel,.setup_personnel)").empty();
    var setup_personnel = "<div class='col-md-4'><div class='tab' id='Contract_Type_pers'><p><i class='fab fa-500px'></i> <a href='/Contract_Type'>Contract type</a></p></div></div><div class='col-md-4'><div class='tab' id='Subscription_Syndicate_pers'><p><i class='fab fa-500px'></i> <a href='Subscription_Syndicate'>Subscription</a></p></div></div><div class='col-md-4'><div class='tab' id='Employee_Recordes_Group_pers'><p><i class='fab fa-500px'></i> <a href='/Employee_Recordes_Group'>Employee recordes type</a></p></div></div><div class='col-md-4'><div class='tab' id='Employee_Profile_Groups_pers'><p><i class='fab fa-500px'></i> <a href='/Employee_Profile_Groups'>Employee profile group</a></p></div></div><div class='col-md-4'><div class='tab' id='check_lists_pers' onclick='checkLists()'><p><i class='fab fa-500px'></i> Check Lists</p></div></div><div class='col-md-4'><div class='tab' id='eos_pers' onclick='EOS()'><p><i class='fab fa-500px'></i> EOS</p></div></div>",
        content = document.querySelector('.setup_personnel'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.setup_personnel').empty();
    } else {
        $('.setup_personnel').append(setup_personnel);
    }
}

function allCardsPersonnel() {
    'use strict';
    $(".child-tabs .row:not(.personnel,.cards_personnel)").empty();
    var cards_personnel = "<div class='col-md-4'><div class='tab' id='/Employee_Profilepers'><p><i class='fab fa-500px'></i> <a href='/Employee_Profile'>Employee Profile</a></p></div></div>",
        content = document.querySelector('.cards_personnel'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.cards_personnel').empty();
    } else {
        $('.cards_personnel').append(cards_personnel);
    }
}

function allTransactionPersonnel() {
    'use strict';
    $(".child-tabs .row:not(.personnel,.transaction_personnel)").empty();
    var transaction_personnel = "<div class='col-md-4'><div class='tab' id='Employee_Profile_pers' onclick='empTrans()'><p><i class='fab fa-500px'></i> Employee Transactions</p></div></div><div class='col-md-4'><div class='tab' id='Employee_records_pers'><p><i class='fab fa-500px'></i> <a href='/Employee_records'>Employee records</a></p></div></div>",
        content = document.querySelector('.transaction_personnel'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.transaction_personnel').empty();
    } else {
        $('.transaction_personnel').append(transaction_personnel);
    }
}

function allProcessPersonnel() {
    'use strict';
    $(".child-tabs .row:not(.personnel,.process_personnel)").empty();
    var process_personnel = "<div class='col-md-4'><div class='tab' id='Employee_Profiles_pers' onclick='empTransProcess()'><p><i class='fab fa-500px'></i> Employee Transactions</p></div></div><div class='col-md-4'><div class='tab' id='Employee_recordss_pers'><p><i class='fab fa-500px'></i> <a href='/Employee_records'>Employee records</a></p></div></div>",
        content = document.querySelector('.process_personnel'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.process_personnel').empty();
    } else {
        $('.process_personnel').append(process_personnel);
    }
}

function allProcessPersonnel() {
    'use strict';
    $(".child-tabs .row:not(.personnel,.process_personnel)").empty();
    var process_personnel = "<div class='col-md-4'><div class='tab' id='Employee_Profiles_pers' onclick='empTrans2()'><p><i class='fab fa-500px'></i> Employee Transactions</p></div></div><div class='col-md-4'><div class='tab' id='Employee_recordss_pers'><p><i class='fab fa-500px'></i> <a href='/Employee_records'>Employee records</a></p></div></div>",
        content = document.querySelector('.process_personnel'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.process_personnel').empty();
    } else {
        $('.process_personnel').append(process_personnel);
    }
}
function allInqueryPersonnel() {
    'use strict';
    $(".child-tabs .row:not(.personnel,.inquery_personnel)").empty();
    var inquery_personnel = "",
        content = document.querySelector('.inquery_personnel'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.inquery_personnel').empty();
    } else {
        $('.inquery_personnel').append(inquery_personnel);
    }
}

function allReportPersonnel() {
    'use strict';
    $(".child-tabs .row:not(.personnel,.report_personnel)").empty();
    var report_personnel = "",
        content = document.querySelector('.report_personnel'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.report_personnel').empty();
    } else {
        $('.report_personnel').append(report_personnel);
    }
}

function checkLists() {
    'use strict';
    $(".child-tabs .row:not(.personnel,.setup_personnel,.check_Lists)").empty();
    var check_Lists = "<div class='col-md-4'><div class='tab' id='Check_Lists_Groups'><p><i class='fab fa-500px'></i> <a href='/Check_Lists_Groups'>Check lists group</a></p></div></div><div class='col-md-4'><div class='tab' id='CheckLists_Items'><p><i class='fab fa-500px'></i> <a href='/CheckLists_Items'>Check lists items</a></p></div></div>",
        content = document.querySelector('.check_Lists'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.check_Lists').empty();
    } else {
        $('.check_Lists').append(check_Lists);
    }
}

function EOS() {
    'use strict';
    $(".child-tabs .row:not(.personnel,.setup_personnel,.eos)").empty();
    var eos = "<div class='col-md-4'><div class='tab' id='Check_Lists_Groups'><p><i class='fab fa-500px'></i> <a href='/EOS_Interview_Questions_Groups'>EOS questions groups</a></p></div></div><div class='col-md-4'><div class='tab' id='CheckLists_Items'><p><i class='fab fa-500px'></i> <a href='/Definition_of_EOS_Interview_Questions'>EOS group</a></p></div></div>",
        content = document.querySelector('.eos'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.eos').empty();
    } else {
        $('.eos').append(eos);
    }
}

function empTrans() {
    'use strict';
    $(".child-tabs .row:not(.personnel,.transaction_personnel,.emptrans)").empty();
    var emptrans = "<div class='col-md-4'><div class='tab' id='personnel_transaction' onclick='personnelTrans()'><p><i class='fab fa-500px'></i> personnel transaction</p></div></div><div class='col-md-4'><div class='tab' id='EOS_request'><p><i class='fab fa-500px'></i> <a href='/EOS_request'>EOS request</a></p></div></div>",
        content = document.querySelector('.emptrans'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.emptrans').empty();
    } else {
        $('.emptrans').append(emptrans);
    }
}

function empTrans2() {
    'use strict';
    $(".child-tabs .row:not(.personnel,.process_personnel,.emptrans2)").empty();
    var emptrans2 = "<div class='col-md-4'><div class='tab' id='personnel_transaction' onclick='personnelTrans2()'><p><i class='fab fa-500px'></i> personnel transaction</p></div></div><div class='col-md-4'><div class='tab' id='EOS_request'><p><i class='fab fa-500px'></i> <a href='/EOS_request'>EOS request</a></p></div></div>",
        content = document.querySelector('.emptrans2'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.emptrans2').empty();
    } else {
        $('.emptrans2').append(emptrans2);
    }
}

function personnelTrans() {
    'use strict';
    $(".child-tabs .row:not(.personnel,.transaction_personnel,.emptrans,.personnel_Trans)").empty();
    var personnel_Trans = "<div class='col-md-4'><div class='tab' id='personnel_transaction'><p><i class='fab fa-500px'></i> <a href='personnel_transaction'>Position transaction</a></p></div></div><div class='col-md-4'><div class='tab' id='request_new_contract'><p><i class='fab fa-500px'></i> <a href='request_new_contract'>Contract transaction</a></p></div></div>",
        content = document.querySelector('.personnel_Trans'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.personnel_Trans').empty();
    } else {
        $('.personnel_Trans').append(personnel_Trans);
    }
}

function personnelTrans2() {
    'use strict';
    $(".child-tabs .row:not(.personnel,.process_personnel,.emptrans2,.personnel_Trans2)").empty();
    var personnel_Trans2 = "<div class='col-md-4'><div class='tab' id='personnel_transaction'><p><i class='fab fa-500px'></i> <a href='personnel_transaction'>Position transaction</a></p></div></div><div class='col-md-4'><div class='tab' id='request_new_contract'><p><i class='fab fa-500px'></i> <a href='request_new_contract'>Contract transaction</a></p></div></div>",
        content = document.querySelector('.personnel_Trans2'),
        result = content.innerHTML.trim();
    
    if (result !== "") {
        $('.personnel_Trans2').empty();
    } else {
        $('.personnel_Trans2').append(personnel_Trans2);
    }
}