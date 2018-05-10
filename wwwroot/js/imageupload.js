/*function handleFileSelect(evt) {
    var files = evt.target.files; // FileList object

    // Loop through the FileList and render image files as thumbnails.
    for (var i = 0, f; f = files[i]; i++) {

      // Only process image files.
      if (!f.type.match('image.*')) {
        continue;
      }

      var reader = new FileReader();

      // Closure to capture the file information.
      reader.onload = (function(theFile) {
        return function(e) {
          // Render thumbnail.
          var span = document.createElement('span');
          span.innerHTML = ['<img class="thumb" src="', e.target.result,
                            '" title="', escape(theFile.name), '"/>'].join('');
          document.getElementById('list').insertBefore(span, null);
        };
      })(f);

      // Read in the image file as a data URL.
      reader.readAsDataURL(f);
    }
  }

  document.getElementById('files').addEventListener('change', handleFileSelect, false);
  */
 /*
  $(document).ready/(function() {
      let onchange = function() {
          let _this =$(this)[0];

          let f = new FormData();
          f.append('File', _this.files[0]);

          let onSuccess = function(model) {
              console.log(model);
          };

          $ajax({
              method: 'POST',
              url: 'account/upload',
              data: f,
              processData: false,
              contentType: false
          }).done(onSuccess);
      };

      $(document).on('change', 'input[type=file]', onchange);
  });
  */