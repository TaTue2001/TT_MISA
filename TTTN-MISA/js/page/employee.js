$(document).ready(function(){
    $(".m-loading").show();
    loadData();  
    var foreMode="edit";
    var EmployeeIDforUpdate=null;
    var check="yes";
    Perform();

    $("#btnAdd").click(function(){
        foreMode="add";
        $("#m-dialog-infor").show();
        $("#txt-EmployeeCode").focus();
        $("#m-dialog-body input").each(function(){  
            ClearInput(this);
        })
        console.log($("#txt-EmployeeCode").val())
    })

    $("#m-dialog-icon-close").click(function(){
        $("#m-dialog-infor" ).hide();
    })

    $(".m-table").on("dblclick","tr",function(){
        foreMode="edit"       
        $("#m-dialog-infor" ).show();
        let employee=$(this).data("entity");
        EmployeeIDforUpdate=employee.EmployeeId;
        let EmployeeCode=$("#txt-EmployeeCode").val(employee.EmployeeCode);
        let DOB=$("#dt-DateOfBirth").val(employee.DateOfBirth);
        let Identification=$("#txt-Identification").val(employee.IdentityNumber);
        let IdentityPlace=$("#txt-PlaceIssued").val(employee.IdentityPlace);
        let Gender=$("#cbx-Gender").val(employee.GenderName);
        let GenderName=$("#cbx-Gender").val(employee.GenderName);
        let PositionName=$("#cbx-Position").val(employee.PositionName);
        let TaxCode=$("#txt-TaxCode").val(employee.PersonalTaxCode);
        let Email=$("#txt-Email").val(employee.Email);
        let FullName=$("#txt-FullName").val(employee.FullName);
        let IdentityDate=$("#dt-DateIssued").val(employee.IdentityDate);
        let PhoneNumber=$("#txt-PhoneNumber").val(employee.PhoneNumber);
        let Salary=$("#txt-Salary").val(employee.Salary);
        let JoinDate=$("#dt-DateJoin").val(employee.JoinDate);
        let DepartmentName=$("#cbx-Department").val(employee.DepartmentName);
        let WorkStatus=$("#cbx-WorkStatus").val(employee.WorkStatus);
        $("#txt-EmployeeCode").focus();
    })

    $("#btn-save").click(function(){
        let EmployeeCode=$("#txt-EmployeeCode").val();
        let DOB=$("#dt-DateOfBirth").val();
        let Identification=$("#txt-Identification").val();
        let IdentityPlace=$("#txt-PlaceIssued").val();
        let Gender=$("#cbx-Gender").val();
        let GenderName=$("#cbx-Gender option:selected").text();
        let PositionName=$("#cbx-Position").val();
        let TaxCode=$("#txt-TaxCode").val();
        let Email=$("#txt-Email").val();
        let FullName=$("#txt-FullName").val();
        let IdentityDate=$("#dt-DateIssued").val();
        let PhoneNumber=$("#txt-PhoneNumber").val();
        let Salary=$("#txt-Salary").val();
        let JoinDate=$("#dt-DateJoin").val();
        let DepartmentName=$("#cbx-Department").val();
        let WorkStatus=$("#cbx-WorkStatus").val();

        if(DOB){
            DOB=new Date(DOB)
        }
        if(DOB>new Date){
            check="no"
            alert("ngay khong duoc phep lon hon ngay hien tai")
      
        }  
        // ValidateDate(DOB);
        if(EmployeeCode===null||EmployeeCode===""||FullName===null||FullName===""||Identification===null||Identification===""||Email===null||Email===""||PhoneNumber===null||PhoneNumber===""){
            alert("Điền đầy đủ các thông tin cần thiết")
            check="no";
            // ValidateInputRequired()
        }
        else{
            if( !validateEmail(Email)) {
                alert("Định dạng email bị sai, mời bạn nhập lại email đúng");   
                $("#txt-Email").focus();
                check="no";
            // $("#txt-Email").addClass("m-infor-detail-input-error");
            }}
        let employee={  
            "EmployeeCode":EmployeeCode,
            "FullName":FullName,
            "GenderName":GenderName,
            "DateOfBirth":DOB,
            "IdentityNumber":Identification,
            "IdentityPlace":IdentityPlace,
            "IdentityDate":IdentityDate,
            "PositionName":PositionName,
            "DepartmentName":DepartmentName,
            "Salary":Salary,
            "Email":Email,
            "JoinDate":JoinDate,
            "PhoneNumber":PhoneNumber,
            "PersonalTaxCode":TaxCode,
            "WorkStatus":WorkStatus,
        }
        if(foreMode==="add" && check==="yes"){
            $.ajax({
            type:"Post",
            url:"https://cukcuk.manhnv.net/api/v1/Employees",
            data:JSON.stringify(employee),
            dataType:"json",
            contentType:"application/json",
            success: function(response){
                $(".m-loading").hide();
                $("#m-dialog-infor" ).hide();
                loadData();
                Perform();
            },
            error: function(response){
            }
            }) 
            // Add(employee);
        }
        if(foreMode==="edit" && check==="yes"){
            Edit(EmployeeIDforUpdate,employee);
        }
        check="yes"
    })
    $("input[requider]").blur(function(){
        var me=this;    
        ValidateInputRequired(me);
    })
})



//Chuẩn hóa định dạng ngày
function ConvertDate(d){
    d=new Date(d);
    let date=d.getDate();
    date=date<10? `0${date}`:date
    let month=d.getMonth()+1;
    month=month<10? `0${month}`:month
    let year=d.getFullYear();
    return d=`${date}/${month}/${year}`;
}

//Chuẩn hóa định dạng tiền tệ
function ConvertCurrency(input){
    return new Intl.NumberFormat('vi-VN', {style: 'currency',currency: 'VND',}).format(input);
}


//Rule validate ngày tháng
// function ValidateDate(d){
//     let check="yes";
//     console.log(d);
//     if(d){
//         d=new Date(d)
//     }
//     if(d>new Date){
//         check="no";
//         alert("ngay khong duoc phep lon hon ngay hien tai")
//     }
//     console.log(check)
// }

//Rule validate email
function validateEmail($email) {
    var emailReg = /^([\w-\.]+@([\w-]+\.)+[\w-]{2,4})?$/;
    return emailReg.test( $email );
}

//Rule validate mã nhân viên


function ValidateInputRequired(input){
    // var me=this;
    let value=$(input).val();
    if (value==null|| value==""){
        $(input).addClass("m-infor-detail-input-error");
        $(input).attr("title","thông tin này không được để trống");
    }
    else {
        $(input).removeClass("m-infor-detail-input-error");
    }
}

//Điều hướng ô input bằng bàn phím
$(document).on('keydown', 'input,select,textarea,a,button', function (e) {
    if (e.which == 37) {
        var tabIndexKey =  document.activeElement.tabIndex-1;
        $('[tabindex=' + tabIndexKey + ']').focus();
    }
    if (e.which == 39) {
        var tabIndexKey =  document.activeElement.tabIndex+1;
        $('[tabindex=' + tabIndexKey + ']').focus();
    }
    if (e.which == 40) {
        var tabIndexKey =  document.activeElement.tabIndex+2;
        $('[tabindex=' + tabIndexKey + ']').focus();
    }
    if (e.which == 38) {
        var tabIndexKey =  document.activeElement.tabIndex-2;
        $('[tabindex=' + tabIndexKey + ']').focus();
    }
});

//Định dạng dữ liệu tiền tệ khi nhập vào
function onlyNumberAmount(input) {
    let v = input.value.replace(/\D+/g, '');
    if (v.length > 20) v = v.slice(0, 20);
    input.value =  v
    .replace(/(^\d{1,3}|\d{3})(?=(?:\d{3})+(?:,|$))/g, '$1.');
}

//Chuyển các ô input về giá trị 0 
function ClearInput(input) {
    $(input).val("")
}

//Hiển thị Toast
function Perform(){
    $("#m-toast-box").find("#m-toast-success").show();
    setTimeout(function() {
        $("#m-toast-box").hide();
    },3000);
}


//Tải dữ liệu
function loadData(){
    $(".m-loading").show();
    $.ajax({
        type:"GET",
        url:"https://cukcuk.manhnv.net/api/v1/Employees",
        success: function(response){
            for (const employee of response) {  
                let employeeCode=employee.EmployeeCode;
                let fullName=employee.FullName;
                let genderName=employee.GenderName;
                let dob=employee.DateOfBirth;
                let phonenb=employee.PhoneNumber;
                let email=employee.Email;
                let position=employee.PositionName;
                let department=employee.DepartmentName;
                let salary=employee.Salary;    
                let workstatus=employee.WorkStatus;                
                if(dob){
                    dob=ConvertDate(dob);
                }    
                // else dob="";  
                if(salary){
                    salary=ConvertCurrency(salary)
                }
                // else salary="";
                var el=$(`<tr> class="m-row-selected">
                            <td class="text-align-left" >${employeeCode}</td>
                            <td class="text-align-left">${fullName}</td>
                            <td class="text-align-left" >${genderName}</td>
                            <td class="text-align-center">${dob}</td>   
                            <td class="text-align-center">${phonenb}</td>
                            <td class="text-align-left" >${email}</td>
                            <td class="text-align-left" >${position}</td>
                            <td class="text-align-left" >${department}</td>
                            <td class="text-align-right" >${salary}</td>
                            <td class="text-align-left" >${workstatus}</td>
                    </tr>'` );
                    el.data("entity",employee);
                $("table#tblEmployee tbody").append(el);
                $(".m-loading").hide(); 
            }   
        },
        error: function(response){
        }
    })
}

//Sửa thông tin nhân viên
function Edit(EmployeeIDforUpdate, entity){
    var EmployeeID=EmployeeIDforUpdate;
    let employee=entity;
    // console.log(employee)
    $.ajax({
        type:"Put",
        url:`https://cukcuk.manhnv.net/api/v1/Employees/${EmployeeID}`,
        data:JSON.stringify(employee),
        dataType:"json",
        contentType:"application/json",
        success: function(response){    
            $(".m-loading").hide();
            Perform();
            $("#m-dialog-infor" ).hide();
            loadData();
        },
        error: function(response){
        }
    }) 
}

//Thêm thông tin nhân viên
function Add(entity){
    let employee=entity;
    console.log(employee);
    $.ajax({
        type:"Post",
        url:"https://cukcuk.manhnv.net/api/v1/Employees",
        data:JSON.stringify(employee),
        dataType:"json",
        contentType:"application/json",
        success: function(response){
            $(".m-loading").hide();
            $("#m-dialog-infor" ).hide();
            loadData();
            Perform();
        },
        error: function(response){
        }
    }) 
}