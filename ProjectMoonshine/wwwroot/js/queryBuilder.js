$(document).ready(function () {
    $('[id^=removeRule]').each(function () {
        $(this).off('click');
        $(this).on("click", function () {
            $(this).closest('.ruleQuery').remove();
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
            reRegisterDeligates();
        });
    });
    test();
});

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

    $('[id^=removeGroup]').each(function () {
        $(this).off('click');
        $(this).on("click", function () {
            $(this).closest('.groupQuery').remove();
            reRegisterDeligates();
        });
    });
    $('[id^=removeRule]').each(function () {
        $(this).off('click');
        $(this).on("click", function () {
            console.log($(this).closest('.ruleQuery').remove());
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
            reRegisterDeligates();
        });
    });
};
function createNewGroup() {
    var id = $('ul[class^=groupQuery]').length;
    var result =
        `<li>
            <ul class="groupQuery">
                <div class="menuBar">
                    <div>
                        <button class="removeGroup" id="removeGroup">- Delete</button>
                        <button class="addRule" id="addRule">+ Rule</button>
                        <button class="addGroup" id="addGroup">+ Group</button>
                    </div >
                    <div>
                        <input value="And" name="toggle_`+ id + `" type="radio" checked>
                        <label>And</label>
                        <input value="Or" name="toggle_`+ id + `" type="radio">
                        <label>Or</label>
                    </div>
                </div>
            </ul>
        </li>`;
    return result;
};

function createNewRule() {
    var result =
        `<li class="ruleQuery">
                <div class="flexRow">
                    <div class="ruleInput">
                        <input id="LeftProperty" type="text" class="ruleInputleft"></input>
                    </div>
                    <div class="equalityComparisonSelect ruleInput" >
                        <select id="EqualityComparison">
                            <option value="1">LessThen</option>
                            <option value="2">GreaterThen</option>
                            <option value="3">LessThenOrEqualsTo</option>
                            <option value="4">GreaterThenOrEqualsTo</option>
                            <option value="5">Equals</option>
                        </select>
                    </div>
                    <div class="ruleInput">
                        <input id="RightProperty" type="text" class="ruleInputright"></input>
                    </div>
                    <div>
                        <button class="removeRule" id="removeRule">- Delete</button>
                    </div>
                </div>
            </li>`;
    return result;
};
function test() {
    var holder = $('#groupsHolder');
    var items = holder.children('ul, li');
    var result = mapRecursively(items);
    console.log(JSON.stringify(result));
}

function mapRecursively(itemList) {
    var logic = $('input[name^="toggle_"]:checked').val();
    var result = {
        logic: logic,
        logicList: searchObject(itemList, logic)
    };
    return result;
}

function searchObject(list, logic){
    var res = { logicGroup: [], logic: logic};
    $.map($(list), function (item) {
        var nodeName = $(item).prop('nodeName');
        if (nodeName === "LI") {
            res.logicGroup.push({
                lhs: $(item).find('#LeftProperty').val(),
                logic: $(item).find("option:selected").text(),
                rhs: $(item).find('#RightProperty').val()
            });
        }
        else if (nodeName === "UL") {
            res.logicGroup.push(searchObject($(item).children('ul, li'), $(item).find('input[name^="toggle_"]:checked').val()));
        }
    });
    return res;
}