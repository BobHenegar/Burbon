function mapRecursively(itemList) {
    var logic = $('input[name^="toggle_"]:checked').val();
    var base = $('#QryTable').val();
    var name = $('#QryName').val();
    var result = {
        queryName: name,
        baseType: base,
        logic: logic,
        logicList: searchObject(itemList, logic)
    };
    return result;
}

function searchObject(list, logic) {
    var res = { logicGroup: [], logic: logic };
    $(list).map(function (index, item) {
        var nodeName = $(item).prop('nodeName');
        if (nodeName === "LI") {
            var leftProp = $(item).find('select#LeftProperty').find("option:selected");
            var rightProp = $(item).find('#RightPropertyInput.ruleInputright');
            var logic = $(item).find('#EqualityComparison').find('option:selected');
            res.logicGroup.push({
                lhs: { type: $(leftProp).data('type'), value: $(leftProp).val() },
                logic: logic.val(),
                rhs: $(rightProp).val() !== "" ? $(rightProp).val() : $(rightProp).text()
            });
        }
        else if (nodeName === "UL") {
            res.logicGroup.push(searchObject($(item).children('ul.groupQuery, li.ruleQuery'), $(item).find('input[name^="toggle_"]:checked').val()));
        }
    });
    return res;
}

function createNewGroup() {
    var id = $('ul[class^=groupQuery]').length;
    var result =
        `<ul class="groupQuery">
            <div class="menuBar">
                <div>
                    <button class="removeGroup" id="removeGroup">- Delete</button>
                    <button class="addRule" id="addRule">+ Rule</button>
                    <button class="addGroup" id="addGroup">+ Group</button>
                </div >
                <div>
                    <input value="1" name="toggle_`+ id + `" type="radio" checked>
                    <label>And</label>
                    <input value="2" name="toggle_`+ id + `" type="radio">
                    <label>Or</label>
                </div>
            </div>
        </ul>`;
    return result;
}

function createNewRule() {
    var result =
        `<li class="ruleQuery">
                <div class="flexRow">
                    <div class="ruleInput" id="LeftPropertyDiv">
                    </div>
                    <div class="equalityComparisonSelect ruleInput" id="EqualityPropertyDiv">
                    </div>
                    <div class="ruleInput" id="RightPropertyDiv">
                    </div>
                    <div  class="ruleInput removeButton">
                        <button class="removeRule" id="removeRule">- Delete</button>
                    </div>
                </div>
            </li>`;
    return result;
}

function submitQueryForm() {
    if (validateBeforePost()) {
        var xhr = new XMLHttpRequest();
        xhr.open('POST', 'Create', true);
        xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
        xhr.onload = function () {
            if (xhr.status === 200) {
                window.location = xhr.responseURL;
            }
            else if (xhr.status === 500) {
                $('#RightPropertyValidation').html('<div class="alert alert-danger" role="alert"><p>I EXPLODED!!!</p></div>');
            }
        };


        var holder = $('#groupsHolder');
        var items = holder.children('ul.groupQuery, li.ruleQuery');
        var result = mapRecursively(items);
        console.log(JSON.stringify(result));
        xhr.send(JSON.stringify(result));
    }
}

function reRegisterDeligates() {
    $("span#RightPropertyValidation").html("");
    $("input#RightPropertyInput").removeClass("dangerInput");
    var addRule = $("[id=addRule]").attr("id", function (i) {
        return "addRule_" + i;
    });
    var addGroup = $("[id=addGroup]").attr("id", function (i) {
        return "addGroup_" + i;
    });
    var removeGroup = $('[id^=removeGroup]').attr("id", function (i) {
        return "removeGroup_" + i;
    });
    var removeRule = $('[id^=removeRule]').attr("id", function (i) {
        return "removeRule_" + i;
    });

    $.each(addRule, function (id, pos) {
        $("#" + id).css(pos);
    });
    $.each(addGroup, function (id, pos) {
        $("#" + id).css(pos);
    });
    $.each(removeGroup, function (id, pos) {
        $("#" + id).css(pos);
    });
    $.each(removeRule, function (id, pos) {
        $("#" + id).css(pos);
    });

    $('[id^=removeRule]').each(function () {
        $(this).off('click');
        $(this).on("click", function () {
            $(this).closest('.ruleQuery').remove();
            reRegisterDeligates();
        });
    });
    $('[id^=removeGroup]').each(function () {
        $(this).off('click');
        $(this).on("click", function () {
            $(this).closest('.groupQuery').remove();
            reRegisterDeligates();
        });
    });
    $('[id^=addGroup]').each(function () {
        $(this).off('click');
        $(this).on("click", function () {

            $(this).closest('.groupQuery').append(createNewGroup());
            reRegisterDeligates();
        });
    });

    $('[id^=addRule]').each(function () {
        $(this).off('click');
        $(this).on("click", function () {
            $(this).closest('.groupQuery').append(createNewRule());
            populateEqualitySelect($(this).closest('.groupQuery'));
            populateLeftSelect($(this).closest('.groupQuery'));
            createRightProperty($(this).closest('.groupQuery'));
            reRegisterDeligates();
        });
    });
    $('[id^=LeftProp]').each(function () {
        $(this).off('change');
        $(this).on('change', function () {
            createRightProperty($(this).parent());
            reRegisterDeligates();
        });
    });
}

function populateNewRightSide(parent) {
    var rhs = $(parent).find('div#RightPropertyDiv').last();
    var lhs = $(parent).find('select#LeftPropertyInput').last();
    $(rhs).empty();
    $(rhs).html('<input id="RightPropertyInput" type="text" class="ruleInputright"></input>');
}

function buildErrorDiv(errorString) {
    return `<div>` + errorString + `</div>`;
}

function validateEquality(inputType, eq) {
    var type = "";
    type = inputType;
    switch (type) {
        case "Guid":
            {
                if (eq !== "5") {
                    return "Valid comparison for a Guid is Equals";
                }
                return "";
            }
        case "Nullable<Guid>":
            {
                if (eq !== "5"
                    && eq !== "7"
                    && eq !== "8"
                    && eq !== "9") {
                    return "Valid comparison(s) for a Nullable Guid is Equals IsNull or IsNotNull";
                }
                return "";
            }
        case "DateTimeOffset":
            {
                if (eq === "6"
                    || eq === "7"
                    || eq === "8"
                    || eq === "9") {
                    return "Valid comparison(s) for a DateTimeOffset is LessThan, GreaterThan, LessThanOrEqualsTo or GreaterThanOrEqualsTo";
                }
                return "";
            }
        case "Nullable<DateTimeOffset>":
            {
                if (eq === "6"
                    || eq === "7") {
                    return "Valid comparison(s) for a Nullable DateTimeOffset is LessThan, GreaterThan, LessThanOrEqualsTo, GreaterThanOrEqualsTo, Equals, IsNull, IsNotNull";
                }
                return "";
            }
        case "Int":
            {
                if (eq === "6"
                    || eq === "7"
                    || eq === "8"
                    || eq === "9") {
                    return "Valid comparison(s) for an Integer is LessThan, GreaterThan, LessThanOrEqualsTo, GreaterThanOrEqualsTo or Equals";
                }
                return "";
            }
        case "Nullable<Int>":
            {
                if (eq === "6"
                    || eq === "7") {
                    return "Valid comparison(s) for a Nullable Integer is LessThan, GreaterThan, LessThanOrEqualsTo, GreaterThanOrEqualsTo, IsNull or IsNotNull";
                }
                return "";
            }
        case "Boolean":
            {
                if (eq !== "5") {
                    return "Valid comparison(s) for a Boolean is Equals";
                }
                return "";
            }
        case "Nullable<Boolean>":
            {
                if (eq !== "5"
                    || eq === "8"
                    || eq === "9") {
                    return "Valid comparison(s) for a Nullable Boolean is Equals, IsNull or Is Not Null";
                }
                return "";
            }
    }
}

function validateInput(type, value) {
    var regex;
    switch (type) {
        case "Guid":
            {
                regex = /^(([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})$)/;
                if (!regex.test(value)) {
                    return false;
                }
                return true;
            }
        case "Nullable<Guid>":
            {
                if (value !== "") {
                    regex = /^(([0-9A-Fa-f]{8}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{4}[-][0-9A-Fa-f]{12})$)/;
                    if (!regex.test(value)) {
                        return false;
                    }
                }
                return true;
            }
        case "DateTimeOffset":
        case "Nullable<DateTimeOffset>":
            {
                if (value !== "") {
                    regex = /^(((([0-9])|(0[1-9])|([12][0-9])|(3[01])){1})([-. /]{1})((([0-9])|([012][0-9])|(3[01])){1})([-. /]{1})([12]{1}[0-9]{3}))/;
                    if (!regex.test(value)) {
                        return false;
                    }
                }
                return true;
            }
        case "Int":
            {
                regex = /^(\d)+/;
                if (!regex.test(value)) {
                    return false;
                }
                return true;
            }
        case "Nullable<Int>":
            {
                if (value !== "") {
                    regex = /^(\d)+/;
                    if (!regex.test(value)) {
                        return false;
                    }
                }
                return true;
            }
        case "Boolean":
            {
                if (value.toLowerCase() !== "true") {
                    return false;
                }
                return true;
            }
        case "Nullable<Boolean>":
            {
                if (value.toLowerCase() !== "") {
                    if (value.toLowerCase() !== "true") {
                        return false;
                    }
                }
                return true;
            }
    }
}
