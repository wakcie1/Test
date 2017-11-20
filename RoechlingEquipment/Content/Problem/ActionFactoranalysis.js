;
(function (window, jQuery, undefined) {

    var ActionFactoranalysis = {
        options: {
            addA1: '#addA1',
            addA2: '#addA2',
            addA3: '#addA3',
            addA4: '#addA4',
            addB1: '#addB1',
            addB2: '#addB2',
            addB3: '#addB3',
            addB4: '#addB4',
            addC1: '#addC1',
            addC2: '#addC2',
            addC3: '#addC3',
            addC4: '#addC4',
            addD1: '#addD1',
            addD2: '#addD2',
            addD3: '#addD3',
            addD4: '#addD4',
            table: '#datatable-editable-factoranalysis',
            problemId: "#proId",
            problemNo: "#problemno",
            dialog: {
                wrapper: '#dialog-factoranalysis',
                cancelButton: '#dialogCancel-factoranalysis',
                confirmButton: '#dialogConfirm-factoranalysis',
            }
        },


        initialize: function () {
            ActionFactoranalysis.setVars()
                .build()
                .events();
            $(window).resize(function () {
                ActionFactoranalysis.drawCanvas();
            });
        },

        setVars: function () {
            this.$table = $(this.options.table);
            this.$addA1 = $(this.options.addA1);
            this.$addA2 = $(this.options.addA2);
            this.$addA3 = $(this.options.addA3);
            this.$addA4 = $(this.options.addA4);
            this.$addB1 = $(this.options.addB1);
            this.$addB2 = $(this.options.addB2);
            this.$addB3 = $(this.options.addB3);
            this.$addB4 = $(this.options.addB4);
            this.$addC1 = $(this.options.addC1);
            this.$addC2 = $(this.options.addC2);
            this.$addC3 = $(this.options.addC3);
            this.$addC4 = $(this.options.addC4);
            this.$addD1 = $(this.options.addD1);
            this.$addD2 = $(this.options.addD2);
            this.$addD3 = $(this.options.addD3);
            this.$addD4 = $(this.options.addD4);
            this.$problemId = $(this.options.problemId);
            this.$problemNo = $(this.options.problemNo);
            // dialog
            this.dialog = {};
            this.dialog.$wrapper = $(this.options.dialog.wrapper);
            this.dialog.$cancel = $(this.options.dialog.cancelButton);
            this.dialog.$confirm = $(this.options.dialog.confirmButton);

            return this;
        },

        build: function () {
            var _self = this;
            this.datatable = this.$table.DataTable({
                aoColumns: [
                    null,
                    null,
                    null,
                    null,
                    null,
                    null,
                    { "bSortable": false }
                ],
                bPaginate: false,
                iDisplayLength: 1000, //默认显示的记录数 
                bInfo: false,
                bFilter: false,
            });

            window.dt = this.datatable;

            setTimeout(function () {
                //draw canvas
                var canvas = $('#canvas')[0];
                var canheight = 400;
                var canwidth = $(".panel-body").width() - 77;
                canvas.height = canheight;
                canvas.width = canwidth;
                var context = canvas.getContext('2d');
                context.moveTo(canwidth * 0, canheight * 0.5);
                context.lineTo(canwidth * 0.8, canheight * 0.5);
                context.moveTo(canwidth * 0, canheight * 0.125);
                context.lineTo(canwidth * 0.3, canheight * 0.5);
                context.moveTo(canwidth * 0, canheight * 0.875);
                context.lineTo(canwidth * 0.3, canheight * 0.5);
                context.moveTo(canwidth * 0.4, canheight * 0.875);
                context.lineTo(canwidth * 0.7, canheight * 0.5);
                context.moveTo(canwidth * 0.4, canheight * 0.125);
                context.lineTo(canwidth * 0.7, canheight * 0.5);
                context.moveTo(canwidth * 0.8, canheight * 0.5);
                context.lineTo(canwidth * 0.78, canheight * 0.48);
                context.moveTo(canwidth * 0.8, canheight * 0.5);
                context.lineTo(canwidth * 0.78, canheight * 0.52);
                context.lineWidth = 1;
                context.strokeStyle = "#000";
                context.stroke();
                _self.$addA1.css("top", 0 - canheight * 0.8);
                _self.$addA2.css("top", 0 - canheight * 0.8).css("left", canwidth * 0.12 - 54);
                _self.$addA3.css("top", 0 - canheight * 0.7).css("left", canwidth * 0.08 - 108);
                _self.$addA4.css("top", 0 - canheight * 0.7).css("left", canwidth * 0.2 - 162);
                _self.$addB1.css("top", 0 - canheight * 0.8 - 24).css("left", canwidth * 0.4);
                _self.$addB2.css("top", 0 - canheight * 0.8 - 24).css("left", canwidth * 0.52 - 54);
                _self.$addB3.css("top", 0 - canheight * 0.7 - 24).css("left", canwidth * 0.48 - 108);
                _self.$addB4.css("top", 0 - canheight * 0.7 - 24).css("left", canwidth * 0.6 - 162);
                _self.$addC1.css("top", 0 - canheight * 0.48).css("left", canwidth * 0.08);
                _self.$addC2.css("top", 0 - canheight * 0.48).css("left", canwidth * 0.2 - 54);
                _self.$addC3.css("top", 0 - canheight * 0.38).css("left", 0 - 108);
                _self.$addC4.css("top", 0 - canheight * 0.38).css("left", canwidth * 0.12 - 162);
                _self.$addD1.css("top", 0 - canheight * 0.48 - 24).css("left", canwidth * 0.48);
                _self.$addD2.css("top", 0 - canheight * 0.48 - 24).css("left", canwidth * 0.6 - 54);
                _self.$addD3.css("top", 0 - canheight * 0.38 - 24).css("left", canwidth * 0.4 - 108);
                _self.$addD4.css("top", 0 - canheight * 0.38 - 24).css("left", canwidth * 0.52 - 162);
            }, 1000);
            return this;
        },

        events: function () {
            var _self = this;

            this.$table
                .on('click', 'a.save-row', function (e) {
                    e.preventDefault();
                    var _event = this;
                    var data = $(_event).closest('tr').find('td').map(function () {
                        var $this = $(this);
                        if ($this.hasClass('actions')) {
                            return _self.datatable.cell(this).data();
                        }
                        else if ($this.hasClass('name')) {
                            var factInfo = {
                                Id: $this.find('label').attr('data-id') || 0,
                                Type: $this.find('label').html() || '',
                            };
                            return factInfo;
                        }
                        else {
                            return $.trim($this.find('input').val());
                        }
                    });
                    var params = {
                        Id: data[0].Id || 0,
                        PAFProblemId: _self.$problemId.val() || 0,
                        PAFProblemNo: _self.$problemNo.html() || '',
                        PAFType: data[0].Type || '',
                        PAFPossibleCause: data[1] || '',
                        PAFWhat: data[2] || '',
                        PAFWhoNo: '',
                        PAFWho: data[3] || '',
                        PAFValidatedDate: data[4] || '',
                        PAFPotentialCause: data[5] || '',
                    };

                    $.ajax({
                        type: "Post",
                        url: "SaveFactorAnalysis",
                        async: true,
                        data: JSON.stringify(params),
                        contentType: "application/json;charset=utf-8",
                        dataType: "json",
                        success: function (data) {
                            if (data != null) {
                                if (data.IsSuccess == true) {
                                    var factInfo = {
                                        Id: data.data.Id,
                                        Type: data.data.PAFType
                                    };
                                    ActionFactoranalysis.rowSave($(_event).closest('tr'), factInfo);
                                }
                                else {
                                    alert("error");
                                }
                            }
                            else {
                                alert("error");
                            }
                        }
                    });
                })
                .on('click', 'a.cancel-row', function (e) {
                    e.preventDefault();

                    _self.rowCancel($(this).closest('tr'));
                })
                .on('click', 'a.edit-row', function (e) {
                    e.preventDefault();

                    _self.rowEdit($(this).closest('tr'));
                })
                .on('click', 'a.remove-row', function (e) {
                    e.preventDefault();
                    var $row = $(this).closest('tr');

                    var dataId = $row.find('.name').find('label').attr('data-id') || 0;
                    $.magnificPopup.open({
                        items: {
                            src: '#dialog-factoranalysis',
                            type: 'inline'
                        },
                        preloader: false,
                        modal: true,
                        callbacks: {
                            change: function () {
                                _self.dialog.$confirm.on('click', function (e) {
                                    e.preventDefault();
                                    _self.rowRecordDelete(dataId);
                                    _self.rowRemove($row);
                                    $.magnificPopup.close();
                                });
                            },
                            close: function () {
                                _self.dialog.$confirm.off('click');
                            }
                        }
                    });
                });

            this.$addA1.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd("A1");
            });

            this.$addA2.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd("A2");
            });

            this.$addA3.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd("A3");
            });

            this.$addA4.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd("A4");
            });

            this.$addB1.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd("B1");
            });

            this.$addB2.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd("B2");
            });

            this.$addB3.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd("B3");
            });

            this.$addB4.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd("B4");
            });

            this.$addC1.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd("C1");
            });

            this.$addC2.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd("C2");
            });

            this.$addC3.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd("C3");
            });

            this.$addC4.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd("C4");
            });

            this.$addD1.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd("D1");
            });

            this.$addD2.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd("D2");
            });

            this.$addD3.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd("D3");
            });

            this.$addD4.on('click', function (e) {
                e.preventDefault();
                _self.rowAdd("D4");
            });

            this.dialog.$cancel.on('click', function (e) {
                e.preventDefault();
                $.magnificPopup.close();
            });
            return this;
        },

        drawCanvas: function () {
            var _self = this;
            //draw canvas
            var canvas = $('#canvas')[0];
            if (!canvas) return false;
            var canheight = 400;
            var canwidth = $(".panel-body").width() - 77;
            canvas.clearRect(0, 0, canwidth, canheight);
            canvas.height = canheight;
            canvas.width = canwidth;
            var context = canvas.getContext('2d');
            context.moveTo(canwidth * 0, canheight * 0.5);
            context.lineTo(canwidth * 0.8, canheight * 0.5);
            context.moveTo(canwidth * 0, canheight * 0.125);
            context.lineTo(canwidth * 0.3, canheight * 0.5);
            context.moveTo(canwidth * 0, canheight * 0.875);
            context.lineTo(canwidth * 0.3, canheight * 0.5);
            context.moveTo(canwidth * 0.4, canheight * 0.875);
            context.lineTo(canwidth * 0.7, canheight * 0.5);
            context.moveTo(canwidth * 0.4, canheight * 0.125);
            context.lineTo(canwidth * 0.7, canheight * 0.5);
            context.moveTo(canwidth * 0.8, canheight * 0.5);
            context.lineTo(canwidth * 0.78, canheight * 0.48);
            context.moveTo(canwidth * 0.8, canheight * 0.5);
            context.lineTo(canwidth * 0.78, canheight * 0.52);
            context.lineWidth = 1;
            context.strokeStyle = "#000";
            context.stroke();
            _self.$addA1.css("top", 0 - canheight * 0.8);
            _self.$addA2.css("top", 0 - canheight * 0.8).css("left", canwidth * 0.12 - 54);
            _self.$addA3.css("top", 0 - canheight * 0.7).css("left", canwidth * 0.08 - 108);
            _self.$addA4.css("top", 0 - canheight * 0.7).css("left", canwidth * 0.2 - 162);
            _self.$addB1.css("top", 0 - canheight * 0.8 - 24).css("left", canwidth * 0.4);
            _self.$addB2.css("top", 0 - canheight * 0.8 - 24).css("left", canwidth * 0.52 - 54);
            _self.$addB3.css("top", 0 - canheight * 0.7 - 24).css("left", canwidth * 0.48 - 108);
            _self.$addB4.css("top", 0 - canheight * 0.7 - 24).css("left", canwidth * 0.6 - 162);
            _self.$addC1.css("top", 0 - canheight * 0.48).css("left", canwidth * 0.08);
            _self.$addC2.css("top", 0 - canheight * 0.48).css("left", canwidth * 0.2 - 54);
            _self.$addC3.css("top", 0 - canheight * 0.38).css("left", 0 - 108);
            _self.$addC4.css("top", 0 - canheight * 0.38).css("left", canwidth * 0.12 - 162);
            _self.$addD1.css("top", 0 - canheight * 0.48 - 24).css("left", canwidth * 0.48);
            _self.$addD2.css("top", 0 - canheight * 0.48 - 24).css("left", canwidth * 0.6 - 54);
            _self.$addD3.css("top", 0 - canheight * 0.38 - 24).css("left", canwidth * 0.4 - 108);
            _self.$addD4.css("top", 0 - canheight * 0.38 - 24).css("left", canwidth * 0.52 - 162);
        },
        // ==========================================================================================
        // ROW FUNCTIONS
        // ==========================================================================================
        rowAdd: function (addName) {

            this.addButtonDisable(addName);

            var actions,
                name,
                possiblecause,
                what,
                who,
                when,
                result,
                data,
                $row;

            actions = [
                '<a href="#" class="hidden on-editing save-row"><i class="fa fa-save"></i></a>',
                '<a href="#" class="hidden on-editing cancel-row"><i class="fa fa-times"></i></a>',
                '<a href="#" class="on-default edit-row"><i class="fa fa-pencil"></i></a>',
                '<a href="#" class="on-default remove-row"><i class="fa fa-trash-o"></i></a>'
            ].join(' ');
            name = [
                '<label class="control-label" data-id="0">' + addName + '</label>',
            ].join(' ');
            possiblecause = [
            ].join(' ');
            what = [
            ].join(' ');
            who = [
            ].join(' ');
            when = [
            ].join(' ');
            result = [
            ].join(' ');
            data = this.datatable.row.add([name, possiblecause, what, who, when, result, actions]);
            $row = this.datatable.row(data[0]).nodes().to$();

            $row
                .addClass('adding')
                .find('td:last')
                .addClass('actions');
            $row.find('td').eq(0)
                .addClass('name');
            $row.find('td').eq(1)
                .addClass('possiblecause');
            $row.find('td').eq(2)
                .addClass('what');
            $row.find('td').eq(3)
                .addClass('who');
            $row.find('td').eq(4)
                .addClass('when');
            $row.find('td').eq(5)
                .addClass('result');

            this.rowEdit($row);

            this.datatable.draw(); // always show fields
            this.datePicker($row);
        },

        rowCancel: function ($row) {
            var _self = this,
                $actions,
                i,
                data;

            if ($row.hasClass('adding')) {
                this.rowRemove($row);
            } else {

                data = this.datatable.row($row.get(0)).data();
                this.datatable.row($row.get(0)).data(data);

                $actions = $row.find('td.actions');
                if ($actions.get(0)) {
                    this.rowSetActionsDefault($row);
                }

                this.datatable.draw();
            }
        },

        rowEdit: function ($row) {
            var _self = this,
                data;

            data = this.datatable.row($row.get(0)).data();

            $row.children('td').each(function (i) {
                var $this = $(this);

                if ($this.hasClass('actions')) {
                    _self.rowSetActionsEditing($row);
                }
                else if ($this.hasClass('when')) {
                    $this.html('<input type="text" class="col-xs-2 form-control width140 dateinput" name="when" value="' + data[i] + '"/>');
                }
                else if ($this.hasClass('name')) {
                    $this.html(data[i]);
                }
                else {
                    $this.html('<input type="text" class="form-control input-block" value="' + data[i] + '"/>');
                }
            });
            this.datePicker($row);
        },

        rowSave: function ($row, factInfo) {
            var _self = this,
                $actions,
                values = [];

            if ($row.hasClass('adding')) {
                //this.$addButton.removeAttr('disabled');
                $row.removeClass('adding');
            }

            values = $row.find('td').map(function () {
                var $this = $(this);

                if ($this.hasClass('actions')) {
                    _self.rowSetActionsDefault($row);
                    return _self.datatable.cell(this).data();
                }
                else if ($this.hasClass('name')) {
                    return $.trim("<label class=\"control-label\" data-id=\"" + factInfo.Id + "\">" + factInfo.Type + "</label >");
                }
                else {
                    return $.trim($this.find('input').val());
                }
            });

            this.datatable.row($row.get(0)).data(values);

            $actions = $row.find('td.actions');
            if ($actions.get(0)) {
                this.rowSetActionsDefault($row);
            }
            this.datatable.draw();
        },

        rowRecordDelete: function (id) {
            if (!id) {
                return false;
            }
            var params = {
                Id: id,
            };
            $.ajax({
                type: "Post",
                url: "InvalidFactorAnalysis",
                async: true,
                data: JSON.stringify(params),
                contentType: "application/json;charset=utf-8",
                dataType: "json",
                success: function (data) {
                }
            });
        },

        rowRemove: function ($row) {
            //if ($row.hasClass('adding')) {
            var addName = $row.find('.name').find('label').html() || '';
            switch (addName) {
                case "A1":
                    this.$addA1.removeAttr('disabled');
                    break;
                case "A2":
                    this.$addA2.removeAttr('disabled');
                    break;
                case "A3":
                    this.$addA3.removeAttr('disabled');
                    break;
                case "A4":
                    this.$addA4.removeAttr('disabled');
                    break;
                case "B1":
                    this.$addB1.removeAttr('disabled');
                    break;
                case "B2":
                    this.$addB2.removeAttr('disabled');
                    break;
                case "B3":
                    this.$addB3.removeAttr('disabled');
                    break;
                case "B4":
                    this.$addB4.removeAttr('disabled');
                    break;
                case "C1":
                    this.$addC1.removeAttr('disabled');
                    break;
                case "C2":
                    this.$addC2.removeAttr('disabled');
                    break;
                case "C3":
                    this.$addC3.removeAttr('disabled');
                    break;
                case "C4":
                    this.$addC4.removeAttr('disabled');
                    break;
                case "D1":
                    this.$addD1.removeAttr('disabled');
                    break;
                case "D2":
                    this.$addD2.removeAttr('disabled');
                    break;
                case "D3":
                    this.$addD3.removeAttr('disabled');
                    break;
                case "D4":
                    this.$addD4.removeAttr('disabled');
                    break;
                default:
            }
            //}
            this.datatable.row($row.get(0)).remove().draw();
        },

        rowSetActionsEditing: function ($row) {
            $row.find('.on-editing').removeClass('hidden');
            $row.find('.on-default').addClass('hidden');
        },

        rowSetActionsDefault: function ($row) {
            $row.find('.on-editing').addClass('hidden');
            $row.find('.on-default').removeClass('hidden');
        },

        datePicker: function ($row) {
            $row.find(".dateinput").each(function (index, element) {
                $(this).datetimepicker({
                    format: "yyyy-mm-dd hh:ii",
                    autoclose: true,
                    todayBtn: true,
                    startDate: "2017-10-01 10:00",
                    minuteStep: 10,
                    minView: 0
                });
            });;
        },

        addButtonDisable: function (addName) {
            var _self = this;
            switch (addName) {
                case "A1":
                    _self.$addA1.attr({ 'disabled': 'disabled' });
                    break;
                case "A2":
                    _self.$addA2.attr({ 'disabled': 'disabled' });
                    break;
                case "A3":
                    _self.$addA3.attr({ 'disabled': 'disabled' });
                    break;
                case "A4":
                    _self.$addA4.attr({ 'disabled': 'disabled' });
                    break;
                case "B1":
                    _self.$addB1.attr({ 'disabled': 'disabled' });
                    break;
                case "B2":
                    _self.$addB2.attr({ 'disabled': 'disabled' });
                    break;
                case "B3":
                    _self.$addB3.attr({ 'disabled': 'disabled' });
                    break;
                case "B4":
                    _self.$addB4.attr({ 'disabled': 'disabled' });
                    break;
                case "C1":
                    _self.$addC1.attr({ 'disabled': 'disabled' });
                    break;
                case "C2":
                    _self.$addC2.attr({ 'disabled': 'disabled' });
                    break;
                case "C3":
                    _self.$addC3.attr({ 'disabled': 'disabled' });
                    break;
                case "C4":
                    _self.$addC4.attr({ 'disabled': 'disabled' });
                    break;
                case "D1":
                    _self.$addD1.attr({ 'disabled': 'disabled' });
                    break;
                case "D2":
                    _self.$addD2.attr({ 'disabled': 'disabled' });
                    break;
                case "D3":
                    _self.$addD3.attr({ 'disabled': 'disabled' });
                    break;
                case "D4":
                    _self.$addD4.attr({ 'disabled': 'disabled' });
                    break;
                default:
            }
        },
        setActionBtn: function (flag) {
            if (flag == 1) {
                $("#factoranalysisbody").find("tr").each(function (index, item) {
                    //$(item).find(".on-editing").each(function (index2, item2) {
                    //    $(item2).removeClass("hidden");
                    //});
                    $(item).find(".on-default").each(function (index2, item2) {
                        $(item2).removeClass("hidden");
                    });
                });
            }
            else if (flag == 0) {
                $("#factoranalysisbody").find("tr").each(function (index, item) {
                    $(item).find(".on-editing").each(function (index2, item2) {
                        $(item2).addClass("hidden");
                    });
                    $(item).find(".on-default").each(function (index2, item2) {
                        $(item2).addClass("hidden");
                    });
                });
            }
        },

        initialdata: function (actionfactoranalysis) {
            var datahtml = "";
            ActionFactoranalysis.datatable.clear();
            $("#factoranalysisbody").html(datahtml);
            for (var i = 0; i < actionfactoranalysis.length; i++) {
                var tr = "<tr role=\"row\" class=\"odd\">" +
                    "<td class=\"name\"><label class=\"control-label\" data-id=\"" + actionfactoranalysis[i].Id + "\">" + actionfactoranalysis[i].PAFType + "</label></td>" +
                    "<td class=\"possiblecause\">" + actionfactoranalysis[i].PAFPossibleCause + "</td>" +
                    "<td class=\"what\">" + actionfactoranalysis[i].PAFWhat + "</td>" +
                    "<td class=\"who\">" + actionfactoranalysis[i].PAFWho + "</td>" +
                    "<td class=\"when\">" + actionfactoranalysis[i].PAFValidatedDateDesc + "</td>" +
                    "<td class=\"result\">" + actionfactoranalysis[i].PAFPotentialCause + "</td>" +
                    "<td class=\"actions\"><a href=\"#\" class=\"hidden on-editing save-row\"><i class=\"fa fa-save\"></i></a>" +
                    " <a href=\"#\" class=\"hidden on-editing cancel-row\"><i class=\"fa fa-times\"></i></a>" +
                    " <a href=\"#\" class=\"on-default edit-row\"><i class=\"fa fa-pencil\"></i></a>" +
                    " <a href=\"#\" class=\"on-default remove-row\"><i class=\"fa fa-trash-o\"></i></a></td></tr>";
                ActionFactoranalysis.datatable.row.add($(tr));
                ActionFactoranalysis.addButtonDisable(actionfactoranalysis[i].PAFType);
            }
            ActionFactoranalysis.datatable.draw();
        }

    };

    window.InitActionFactoranalysis = ActionFactoranalysis.initialize;
    window.InitActionFactoranalysisData = ActionFactoranalysis.initialdata;
    window.SetActionFactoranalysisBtn = ActionFactoranalysis.setActionBtn;
})(window, jQuery)


