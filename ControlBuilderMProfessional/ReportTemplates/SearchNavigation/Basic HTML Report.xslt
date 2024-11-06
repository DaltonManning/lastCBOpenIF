<?xml version="1.0" encoding="UTF-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns="http://www.w3.org/1999/xhtml">
	<xsl:output method="html" version="1.0" encoding="UTF-8" indent="yes"/>
	
	<xsl:variable name="nbrSymbols" select="count(/Symbols/Symbol)"></xsl:variable>
	
  <xsl:template match="Symbols">
    <html>
      <head>
        <title>Search and Navigation report</title>
      </head>
      <body bgcolor="#FFFFFF" text="#000099" alink="#0000FF" vlink="#0000FF">
        <h2>Search and Navigation report for project <xsl:value-of select="@ProjectName" /></h2>
        <h3><xsl:value-of select="@Date" /> - <xsl:value-of select="@Time" /></h3>
        <table border="1" cellpadding="2" cellspacing="0">
          <tbody>
            <tr>
              <td colspan="2" bgcolor="#000099"><b><font color="#FFFF00">Search settings</font></b></td>
            </tr>
            <tr>
              <td><b>For</b></td>
              <td><xsl:value-of select="@SearchFor" /><br /></td>
            </tr>
            <tr>
              <td><b>In</b></td>
              <td><xsl:value-of select="@SearchIn" /><br /></td>
            </tr>
            <tr>
              <td><b>Match</b></td>
              <td><xsl:value-of select="@MatchType" /><br /></td>
            </tr>
            <tr>
              <td><b>Filter</b></td>
              <td><xsl:value-of select="@Filter" /><br /></td>
            </tr>
            <tr>
              <td><b>Max no of hits</b></td>
              <td><xsl:value-of select="@MaxNumberOfHits" /><br /></td>
            </tr>
            <tr>
              <td><b>Number of hits</b></td>
              <td><xsl:value-of select="count(Symbol)" /><br /></td>
            </tr>
          </tbody>
        </table>
        <br />
        <table border="1" cellpadding="2" cellspacing="0">
          <tr bgcolor="#000099">
            <td>
              <b><font color="#FFFF00">Symbol</font></b>      
            </td>
            <td>
              <b><font color="#FFFF00">Type</font></b>      
            </td>
            <td>
              <b><font color="#FFFF00">Path</font></b>      
            </td>
          </tr>
          <xsl:apply-templates mode="list"/>
        </table>
        <br />
        <table border="1" cellpadding="2" cellspacing="0">
          <xsl:apply-templates />
        </table>
      </body>
    </html>
  </xsl:template>

  <xsl:template match="Symbol"> 
   <tbody>
    <tr>
      <td colspan="2" bgcolor="#000099">
        <a name="{generate-id()}">
        <b><font color="#FFFF00">Symbol</font></b>      
        </a>
      </td>
    </tr>
    <tr>
      <td bgcolor="#CCCCCC">
        <b>Name</b>      
      </td>
      <td bgcolor="#CCCCCC">
        <b>Type</b>      
      </td>
    </tr>
    <tr>
      <td>
        <xsl:value-of select="@Name" />      
      </td>
      <td>
        <xsl:value-of select="@Type" />      
      </td>
    </tr>
    <xsl:apply-templates />
    <xsl:choose>
      <xsl:when test="position() != $nbrSymbols">
        <tr><td colspan="2" ><br /></td></tr>  
      </xsl:when>
    </xsl:choose>
   </tbody>
  </xsl:template>

  <xsl:template match="Symbol" mode="list"> 
    <tr>
      <td>
        <a href="#{generate-id()}">
        <xsl:value-of select="@Name" />      
        </a>
      </td>
      <td>
        <xsl:value-of select="@Type" />      
      </td>
      <xsl:apply-templates mode="list"/>     
    </tr>
  </xsl:template>

  <xsl:template match="Definition">
    <tr>
      <td colspan="2" bgcolor="#CCCCCC">
        <b>Definition</b>
      </td>
    </tr>
    <tr>
      <td>
        <b>Path</b>       
      </td>
      <td>
        <b>Location</b> 
      </td>
    </tr>
    <tr>
      <td valign="top">
        <xsl:value-of select="@Path" />      
      </td>
      <td>
        <xsl:apply-templates /><br />
      </td>
    </tr>
  </xsl:template>

  <xsl:template match="Definition" mode="list">
      <td valign="top">
        <xsl:value-of select="@Path" />      
      </td>
  </xsl:template>

  <xsl:template match="References">
    <tr>
      <td colspan="2" bgcolor="#CCCCCC">
        <b>References</b><br />
      </td>
    </tr>
    <tr>
      <td>
        <b>Path</b>       
      </td>
      <td>
        <b>Location</b> 
      </td>
    </tr>
    <xsl:apply-templates />
  </xsl:template>

  <xsl:template match="Reference">
    <tr>
      <td valign="top">
        <xsl:value-of select="@Path" />      
      </td>
      <td>
      <xsl:choose>
        <xsl:when test="@Attribute">
          Attribute = <xsl:value-of select="@Attribute" /><br />      
        </xsl:when>
      </xsl:choose>
        <xsl:apply-templates />
      </td>
    </tr>
  </xsl:template>

  <xsl:template match="Location">
    <xsl:choose>
      <xsl:when test="@Tab">
        Tab = <xsl:value-of select="@Tab" /><br />      
      </xsl:when>
    </xsl:choose>
    <xsl:choose>
    	 <xsl:when test="@Row">
        Row = <xsl:value-of select="@Row" /><br />      
    	 </xsl:when>
    </xsl:choose>
  </xsl:template>

</xsl:stylesheet>
