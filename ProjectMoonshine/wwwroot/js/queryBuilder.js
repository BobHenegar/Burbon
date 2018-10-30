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