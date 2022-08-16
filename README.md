# OnlineOCR dll

線上OCR

此份文件包含可以使用的 Function 與回傳的 Class

## Function

- ### StartOCR(string , bool, string)

  * FilePath：需要辨識的檔案位置

  * Saveimg：是否儲存圖片

  * TaxID：我方公司統一編號

## Return Class Information

- ### Receipt:

  - Sysinfo
  - Result

- ### Sysinfo: 

  - Sentence：全文結果
  - Saveimg：是否刪除圖檔
  - Type：解析方式
  - "essage：說明

- ### Result: 

  - Date：日期
  - ReceiptNum：收據號碼
  - SellerNum：賣方統編
  - BuyerNum：買方統編
  - NoTaxCharge：未稅金額
  - Charge：含稅金額(總計)
  - Tax：稅額
  - ReceiptFormat：發票格式

