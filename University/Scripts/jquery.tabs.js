/* =================================================
// jQuery Tabs Plugins 1.0
// author:chenyg@5173.com
// URL:http://stylechen.com/jquery-tabs.html
// 4th Dec 2010
// =================================================*/

(function($){
	$.fn.extend({
		TabsIndex1:function(options){
			// 处理参数
			options = $.extend({
				event : 'mouseover',
				timeout : 0,
				auto : 0,
				callback : null
			}, options);
			
			var self = $(this),
				tabBox = self.children('div.tab_Index1_box').children('div.tab_Index1_box_item'),
				tabBoxLeft = $('#tab_Index1_box2').children('div.tab_Index1_box2_item'),           //图片文字div
				menu = self.children('ul.tab_Index1_menu'),
				items = menu.find( 'li' ),
				timer;
				
			var tabHandle = function( elem ){
					elem.siblings( 'li' )
						.removeClass( 'current' )
						.end()
						.addClass( 'current' );

					tabBox.siblings('div.tab_Index1_box_item')
						.addClass( 'hide' )
						.end()
						.eq( elem.index() )
						.removeClass( 'hide' );

					tabBoxLeft.siblings('div.tab_Index1_box2_item')    //图片文字div
						.addClass('hide')
						.end()
						.eq(elem.index())
						.removeClass('hide');
				},
					
				delay = function( elem, time ){
					time ? setTimeout(function(){ tabHandle( elem ); }, time) : tabHandle( elem );
				},
				
				start = function(){
					if( !options.auto ) return;
					timer = setInterval( autoRun, options.auto );
				},
				
				autoRun = function(){
					var current = menu.find( 'li.current' ),
						firstItem = items.eq(0),
						len = items.length,
						index = current.index() + 1,
						item = index === len ? firstItem : current.next( 'li' ),
						i = index === len ? 0 : index;
					
					current.removeClass( 'current' );
					item.addClass( 'current' );

					tabBox.siblings('div.tab_Index1_box_item')
						.addClass( 'hide' )
						.end()
						.eq(i)
						.removeClass('hide');
						
					tabBoxLeft.siblings('div.tab_Index1_box2_item')    //图片文字div
						.addClass('hide')
						.end()
						.eq(i)
						.removeClass('hide');                          
				};
							
			items.bind( options.event, function(){
				delay( $(this), options.timeout );
				if( options.callback ){
					options.callback( self );
				}
			});
			
			if( options.auto ){
				start();
				self.hover(function(){
					clearInterval( timer );
					timer = undefined;
				},function(){
					start();
				});
			}
			
			return this;
		}
	});
})(jQuery);

(function($) {
    $.fn.extend({
        TabsIndex2: function(options) {
            // 处理参数
            options = $.extend({
                event: 'mouseover',
                timeout: 0,
                auto: 0,
                callback: null
            }, options);

            var self = $(this),
				tabBox = self.children('div.tab_Index2_box').children('div.tab_Index2_box_item'),
				menu = self.children('ul.tab_Index2_menu'),
				items = menu.find('li'),
				timer;

            var tabHandle = function(elem) {
                elem.siblings('li')
						.removeClass('current')
						.end()
						.addClass('current');

                tabBox.siblings('div.tab_Index2_box_item')
						.addClass('hide')
						.end()
						.eq(elem.index())
						.removeClass('hide');
            },

				delay = function(elem, time) {
				    time ? setTimeout(function() { tabHandle(elem); }, time) : tabHandle(elem);
				},

				start = function() {
				    if (!options.auto) return;
				    timer = setInterval(autoRun, options.auto);
				},

				autoRun = function() {
				    var current = menu.find('li.current'),
						firstItem = items.eq(0),
						len = items.length,
						index = current.index() + 1,
						item = index === len ? firstItem : current.next('li'),
						i = index === len ? 0 : index;

				    current.removeClass('current');
				    item.addClass('current');

				    tabBox.siblings('div.tab_Index2_box_item')
						.addClass('hide')
						.end()
						.eq(i)
						.removeClass('hide');
				};

            items.bind(options.event, function() {
                delay($(this), options.timeout);
                if (options.callback) {
                    options.callback(self);
                }
            });

            if (options.auto) {
                start();
                self.hover(function() {
                    clearInterval(timer);
                    timer = undefined;
                }, function() {
                    start();
                });
            }

            return this;
        }
    });
})(jQuery);