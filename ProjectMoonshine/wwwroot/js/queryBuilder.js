function submitQueryForm() {
    var xhr = new XMLHttpRequest();
    xhr.open('POST', 'Create', true);
    xhr.setRequestHeader('Content-Type', 'application/json; charset=UTF-8');
    xhr.onload = function () {
        if (xhr.status === 200) {
            window.location = xhr.responseURL;
        }
    };

    var holder = $('#groupsHolder');
    var items = holder.children('ul.groupQuery, li.ruleQuery');
    var result = mapRecursively(items);
    console.log(JSON.stringify(result));
    xhr.send(JSON.stringify(result))
}

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
            var rightProp = $(item).find('input#RightPropertyInput');
            var logic = $(item).find('#EqualityComparison').find('option:selected');
            res.logicGroup.push({
                lhs: { type: $(leftProp).data('type'), value: $(leftProp).val() },
                logic: logic.val(),
                rhs: $(rightProp).text() === "" ? $(rightProp).val() : $(rightProp).text()
            });
        }
        else if (nodeName === "UL") {
            res.logicGroup.push(searchObject($(item).children('ul.groupQuery, li.ruleQuery'), $(item).find('input[name^="toggle_"]:checked').val()));
        }
    });
    return res;
}


function reRegisterDeligates() {
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
            refreshDropDowns();
            reRegisterDeligates();
        });
    });
    $('[id^=removeGroup]').each(function () {
        $(this).off('click');
        $(this).on("click", function () {
            $(this).closest('.groupQuery').remove();
            refreshDropDowns();
            reRegisterDeligates();
        });
    });

    $('[id^=addGroup]').each(function () {
        $(this).off('click');
        $(this).on("click", function () {

            $(this).closest('.groupQuery').append(createNewGroup());
            refreshDropDowns();
            reRegisterDeligates();
        });
    });

    $('[id^=addRule]').each(function () {
        $(this).off('click');
        $(this).on("click", function () {
            $(this).closest('.groupQuery').append(createNewRule());
            refreshDropDowns();
            reRegisterDeligates();
        });
    });
    refreshDropDowns();
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
                    <div class="ruleInput">
                        <select id="LeftProperty"></select>
                    </div>
                    <div class="equalityComparisonSelect ruleInput" >
                       @Html.DropDownList("EqualityComparison", new SelectList(ViewBag.EqualityComparisonsList, "Key", "Value", "4"), null, new { id = "EqualityComparison", })
                    </div>
                    <div class="ruleInput" id="RightProperty">
                        <input id="RightPropertyInput" type="text" class="ruleInputright"></input>
                        <span id="RightPropertyValidation" class="text-danger"></span>
                    </div>
                    <div  class="ruleInput">
                        <button class="removeRule" id="removeRule">- Delete</button>
                    </div>
                </div>
            </li>`;
    return result;
}