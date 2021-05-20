var inst = (() => {
    const template = (item) => `<div class="col-md-4">

                        <!--PRICE CONTENT START-->
                        <div class="generic_content clearfix">

                            <!--HEAD PRICE DETAIL START-->
                            <div class="generic_head_price clearfix" style="background-image:url('/img/university_icon_2.jpg');background-size: contain;background-size: 100% 100%;position:relative;">

                                <!--HEAD CONTENT START-->
                                <div class="generic_head_content clearfix">

                                    <!--HEAD START-->
                                    <div class="head_bg"></div>
                                    <div class="head">
                                        <span>Institution Name Can be very long text</span>
                                    </div>
                                    <!--//HEAD END-->
                                    <!--PRICE START-->
                                    <div class="generic_price_tag clearfix">
                                        <span class="price">
                                            <span class="sign"></span>
                                            <span class="currency"></span>
                                            <span class="cent"></span>
                                            <span class="month"></span>
                                        </span>
                                    </div>
                                    <!--//PRICE END-->
                                </div>
                                <!--//HEAD CONTENT END-->
                                <a style="position:absolute;right:5px;bottom: 5px;" href="javascript:void(0);"><img class="img-upl" src="/img/upload_1.png" width="40" style="" /></a>
                            </div>
                            <!--//HEAD PRICE DETAIL END-->
                            <!--FEATURE LIST START-->
                            <div class="generic_feature_list">
                                <ul>
                                    <li><label>Type</label> University</li>
                                    <li><label>Affiliation</label> Affiliated University</li>
                                    <li><label>Address</label> Full AddressFull AddressFull AddressFull AddressFull AddressFull AddressFull AddressFull Address AddressFull Address AddressFull Address</li>
                                    <li><label>Contact</label>Contact</li>
                                    <li><label>Email</label> Email@tetet.com</li>
                                </ul>
                            </div>
                            <!--//FEATURE LIST END-->
                            <!--BUTTON START-->
                            <div class="generic_price_btn clearfix">
                                <a class="javascript:void(0);" href="">Details</a>
                                <a class="javascript:void(0);" href="">Report</a>
                            </div>
                            <!--//BUTTON END-->

                        </div>
                        <!--//PRICE CONTENT END-->

                    </div>`
    let listcontain = document.querySelector(".tile-container");
    let $file_upl = $("#file_upl");
    $file_upl.on('change', (evt) => {
        var file = evt.target.files[0];
        ArkLog('FileUpl', file).log();
    });
    $(document).on("click", ".img-upl", (evt) => {
        var itm = $(evt.target).closest(".col-md-4").data_item;
        $file_upl.val(null);
        $file_upl.click();
    });
    return {
        render: (data) => {
            data = Array.isArray(data) ? data : (data ? [data] : []);
            data.forEach(t => {
                let dom = util.textToDom(template(t));
                dom.data_item = t;
                listcontain.appendChild(dom);
            });
        }
    }
})();