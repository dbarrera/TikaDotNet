﻿using System;
using java.io;
using javax.xml.transform;
using javax.xml.transform.sax;
using javax.xml.transform.stream;
using org.apache.tika.metadata;
using org.apache.tika.parser;
using Parser = org.apache.tika.parser.Parser;

namespace TikaDotNet.TextExtraction.Stream
{
    public class StreamTextExtractor
    {
        public Metadata Extract(Func<Metadata, InputStream> streamFactory, System.IO.Stream outputStream)
        {
            try
            {
                var parser = new AutoDetectParser();
                var metadata = new Metadata();
                var parseContext = new ParseContext();
                var handler = GetTransformerHandler(outputStream);

                //use the base class type for the key or parts of Tika won't find a usable parser
                parseContext.set(typeof(Parser), parser);

                using (var inputStream = streamFactory(metadata))
                {
                    try
                    {
                        parser.parse(inputStream, handler, metadata, parseContext);
                    }
                    finally
                    {
                        inputStream.close();
                    }
                }

                return metadata;
            }
            catch (Exception ex)
            {
                throw new TextExtractionException("Extraction failed.", ex);
            }
        }

        private static TransformerHandler GetTransformerHandler(System.IO.Stream outputStream)
        {
            var factory = (SAXTransformerFactory)TransformerFactory.newInstance();
            var transformerHandler = factory.newTransformerHandler();

            transformerHandler.getTransformer().setOutputProperty(OutputKeys.METHOD, "text");
            transformerHandler.getTransformer().setOutputProperty(OutputKeys.INDENT, "no");

            var outputAdapter = new DotNetOutputStreamAdapter(outputStream);
            transformerHandler.setResult(new StreamResult(outputAdapter));
            return transformerHandler;
        }
    }
}
